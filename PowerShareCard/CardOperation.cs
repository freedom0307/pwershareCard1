using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DK;
using System.Threading;
using System.Collections;
using System.Reflection;
using Model;
using System.Diagnostics;
using System.IO;


namespace PowerShareCard
{
    public class Cardoperation
    {
        private static Cardoperation Instance;
        private static object _lock = new object();
        private static object _lock1 = new object();
        public byte[] Create_ard = new byte[11] { 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };//办卡，文档有误，初始值是5个字节，总共11个字节
        public byte[] Cardoperate = new byte[10] { 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};//充值和扣款
        public byte[] HFCard = new byte[9] { 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };//恢复卡，文档有误，后面再加0x38、0x52、0x7A三个字节，总共9个字节
        byte[] Check_card = new byte[10] { 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };//读卡
        byte[] New_Baudrate = new byte[13] { 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        byte[] Load_Key = new byte[23] { 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        byte[] ReadBlock = new byte[6] { 0x7F, 0x04, 0x00, 0x11, 0x01, 0x14 };//读块操作
        byte N_7F_num = 0;
        Stopwatch stopwatch = new Stopwatch();
        TimeSpan ts ;
        Queue myQ = new Queue();
        private List<byte> buffer = new List<byte>(4096);
        private List<byte> bufferTemporary = new List<byte>(100);//发送数据暂存，主要用于处理转义字符7F
        private List<byte> bufferReceveData = new List<byte>(100);//接收数据暂存，主要用于处理转义字符7F
        public Semaphore semaSerialPort = new Semaphore(1, 1); //串口读写锁
        public Semaphore semaSendRecieve = new Semaphore(1, 1);//协议收发锁
        public delegate void DataHandle();
        //DataHandle interfaceUpdateHandle;
        public MySerialPort serialport1 = new MySerialPort();
        volatile Int32 Jine;
        volatile Int32 Cash;
        public bool Flag = false, NewCard_flag = false, HuiFCard_flag = false;
        ArrayList arraylist = new ArrayList();
        CardInfo Card_Info = new CardInfo();



        public Cardoperation()
        {
            serialport1.ResceiveMessage += serialport1_ResceiveMessage;
            //System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(Application.StartupPath + @"/pm3.wav");
        }

        public static Cardoperation GetInstance()
        {
            if (Instance == null)
            {
                lock (_lock)
                {
                    if (Instance == null)
                    {
                        Instance = new Cardoperation();
                    }
                }
            }
            return Instance;
        }
        public CardInfo ReadCard(string CardID, Int32 SUM = 0)//读卡
        {
            stopwatch.Start();

            int k = 0;
            try
            {
                ClearCardInfo(Card_Info);
                Int32 f1 = SUM * 256;
                byte[] bytes = intToBytes2(f1);
                Check_card[1] = 0x08;
                Check_card[3] = 0x16;
                Check_card[4] = 0x01;
                for (int i = 0; i < 4; i++)
                {
                    Check_card[5 + i] = bytes[i];
                }

                Check_card[9] = checkSum(Check_card, 1, 8);
                byte flag7FNum = check7F(Check_card, 1, 9);
                if (flag7FNum != 0)
                {
                    int N = Check_card.Length + flag7FNum;
                    bufferTemporary.Add(Check_card[0]);
                    for (int j = 1; j <Check_card .Length ; j++)
                    {
                        bufferTemporary .Add (Check_card [j]);
                        if (Check_card [j]==0x7F)
                        {
                            bufferTemporary.Add(Check_card[j]);
                        }
                    }
                    serialport1.WriteByteToSerialPort(bufferTemporary .ToArray ()  , N );
                }
                else
                {
                    serialport1.WriteByteToSerialPort(Check_card, 10);
                }
                bufferTemporary.RemoveRange(0, bufferTemporary.Count);

            }
            catch (Exception error)
            {
                //Card_Info.Message = error.Message;
                Card_Info.Message = "未知异常";
                Card_Info.Code = -3;
            }
            while (true)
            {
                Thread.Sleep(700);
                if (k >= 10 || Card_Info.Code != 0xFF)
                    break;
                k++;
            }
            return Card_Info;

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }
        public CardInfo NewCard(string CardID, Int32 SUM)//办卡
        {
            stopwatch.Start();
            int k = 0;
            try
            {
                ClearCardInfo(Card_Info);
                Int32 f1 = SUM * 256 + 256255;
                byte[] bytes = intToBytes2(f1);
                Create_ard[1] = 0x09;
                Create_ard[3] = 0x13;
                Create_ard[4] = 0x01;
                for (int i = 0; i < 4; i++)
                {
                    Create_ard[6 + i] = bytes[i];
                }
                Create_ard[10] = checkSum(Create_ard, 1, 9);
                byte flag7FNum = check7F(Create_ard, 1, 10);
                if (flag7FNum != 0)
                {
                    int N = Create_ard.Length + flag7FNum;
                    bufferTemporary.Add(Create_ard[0]);
                    for (int j = 1; j < Create_ard.Length; j++)
                    {
                        bufferTemporary.Add(Create_ard[j]);
                        if (Create_ard[j] == 0x7F)
                        {
                            bufferTemporary.Add(Create_ard[j]);
                        }
                    }
                    serialport1.WriteByteToSerialPort(bufferTemporary.ToArray(), N);
                }
                else
                {
                    serialport1.WriteByteToSerialPort(Create_ard, 11);
                }
                
            }
            catch (Exception erro)
            {
                // Card_Info.Message = erro.Message;
                Card_Info.Message = "未知异常";
                Card_Info.Code = -3;
            }
            while (true)
            {
                Thread.Sleep(700);
                if (k >= 10 || Card_Info.Code != 0xFF)
                    break;
                k++;

            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }
        public CardInfo Recharge(string CardID, Int32 SUM)//充值
        {
            stopwatch.Start();
            int k = 0;
            try
            {
                ClearCardInfo(Card_Info);
                Int32 f1 = SUM * 256;
                byte[] bytes1 = intToBytes2(f1);
                byte[] bytes=System.BitConverter.GetBytes(f1);
                Cardoperate[1] = 0x08;
                Cardoperate[3] = 0x15;
                Cardoperate[4] = 0x01;
                for (int i = 0; i < 4; i++)
                {
                    Cardoperate[5 + i] = bytes1[i];
                }
                Cardoperate[9] = checkSum(Cardoperate, 1, 8);
                byte flag7FNum = check7F(Cardoperate, 1, 9);
                if (flag7FNum != 0)
                {
                    int N = Cardoperate.Length + flag7FNum;
                    bufferTemporary.Add(Cardoperate[0]);
                    for (int j = 1; j < Cardoperate.Length; j++)
                    {
                        bufferTemporary.Add(Cardoperate[j]);
                        if (Cardoperate[j] == 0x7F)
                        {
                            bufferTemporary.Add(Cardoperate[j]);
                        }
                    }
                    serialport1.WriteByteToSerialPort(bufferTemporary.ToArray(), N);
                }
                else
                {
                    serialport1.WriteByteToSerialPort(Cardoperate, 10);

                }
                bufferTemporary.RemoveRange(0,bufferTemporary .Count );
               
            }
            catch (Exception erro)
            {
                // Card_Info.Message = erro.Message;
                Card_Info.Message = "未知异常";
                Card_Info.Code = -3;
            }
            while (true)
            {
                Thread.Sleep(700);
                if (k >= 10 || Card_Info.Code != 0xFF)
                    break;
                k++;

            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

        }
        public CardInfo SwipingCard(string CardID, Int32 SUM)//刷卡
        {
            stopwatch.Start();
            int k = 0;
            try
            {
                ClearCardInfo(Card_Info);
                Int32 f1 = SUM * 256;
                byte[] bytes = intToBytes2(f1);
                Cardoperate[1] = 0x08;
                Cardoperate[3] = 0x16;
                Cardoperate[4] = 0x01;
                for (int i = 0; i < 4; i++)
                {
                    Cardoperate[5 + i] = bytes[i];
                }
                Cardoperate[9] = checkSum(Cardoperate, 1, 8);
                byte flag7FNum = check7F(Cardoperate, 1, 9);
                if (flag7FNum != 0)
                {
                    int N = Cardoperate.Length + flag7FNum;
                    bufferTemporary.Add(Cardoperate[0]);
                    for (int j = 1; j < Cardoperate.Length; j++)
                    {
                        bufferTemporary.Add(Cardoperate[j]);
                        if (Cardoperate[j] == 0x7F)
                        {
                            bufferTemporary.Add(Cardoperate[j]);
                        }
                    }
                    serialport1.WriteByteToSerialPort(bufferTemporary.ToArray(), N);
                }
                else
                {
                    serialport1.WriteByteToSerialPort(Cardoperate, 10);
                }
                bufferTemporary.RemoveRange(0, bufferTemporary.Count);
                
            }
            catch (Exception erro)
            {
                //Card_Info.Message = erro.Message;
                Card_Info.Message = "未知异常";
                Card_Info.Code = -3;
            }
            while (true)
            {
                Thread.Sleep(700);
                if (k >= 10 || Card_Info.Code != 0xFF)
                    break;
                k++;
            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

        }
        public CardInfo HuiFCard(string CardID, Int32 SUM)//恢复卡
        {
            stopwatch.Start();
            int k = 0;
            try
            {
                ClearCardInfo(Card_Info);
                HFCard[1] = 0x07;
                HFCard[3] = 0x14;
                HFCard[4] = 0x01;
                HFCard[5] = 0x38;
                HFCard[6] = 0x52;
                HFCard[7] = 0x7A;
                HFCard[8] = checkSum(HFCard, 1, 7);
                byte flag7FNum = check7F(HFCard, 1, 8);
                if (flag7FNum != 0)
                {
                    int N = HFCard.Length + flag7FNum;
                    bufferTemporary.Add(HFCard[0]);
                    for (int j = 1; j < HFCard.Length; j++)
                    {
                        bufferTemporary.Add(HFCard[j]);
                        if (HFCard[j] == 0x7F)
                        {
                            bufferTemporary.Add(HFCard[j]);
                        }
                    }
                    serialport1.WriteByteToSerialPort(bufferTemporary.ToArray(), N);
                }
                else
                {
                    serialport1.WriteByteToSerialPort(HFCard, 9);
                }
                bufferTemporary.RemoveRange(0, bufferTemporary.Count);
            }
            catch (Exception erro)
            {
                //Card_Info.Message = erro.Message;
                Card_Info.Message = "未知异常";
                Card_Info.Code = -3;
            }
            while (true)
            {
                Thread.Sleep(700);
                if (k >= 10 || Card_Info.Code != 0xFF)
                    break;
                k++;
            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }

        public CardInfo ChangeBaudrate(string CardID, Int32 BaudRate)//改变波特率
        {
            stopwatch.Start();
            int k = 0;
            try
            {
                ClearCardInfo(Card_Info);
                byte[] bytes = intToBytes2(BaudRate);
                New_Baudrate[1] = 0x0A;
                New_Baudrate[3] = 0x2C;
                for (int i = 0; i < 4; i++)
                {
                    New_Baudrate[4 + i] = bytes[i];
                }
                New_Baudrate[8] = 0x98;
                New_Baudrate[9] = 0x24;
                New_Baudrate[10] = 0x31;
                New_Baudrate[11] = checkSum(New_Baudrate, 1, 10);
                byte flag7FNum = check7F(New_Baudrate, 1, 11);
                if (flag7FNum != 0)
                {
                    int N = New_Baudrate.Length + flag7FNum;
                    bufferTemporary.Add(New_Baudrate[0]);
                    for (int j = 1; j < New_Baudrate.Length; j++)
                    {
                        bufferTemporary.Add(New_Baudrate[j]);
                        if (New_Baudrate[j] == 0x7F)
                        {
                            bufferTemporary.Add(New_Baudrate[j]);
                        }
                    }
                    serialport1.WriteByteToSerialPort(bufferTemporary.ToArray(), N);
                }
                else
                {
                    serialport1.WriteByteToSerialPort(New_Baudrate, 12);
                }


                bufferTemporary.RemoveRange(0, bufferTemporary.Count);

            }
            catch (Exception error)
            {
                //Card_Info.Message = error.Message;
                Card_Info.Message = "未知异常";
                Card_Info.Code = -3;
            }
            while (true)
            {
                Thread.Sleep(700);
                if (k >= 10 || Card_Info.Code != 0xFF)
                    break;
                k++;
            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }

        public CardInfo LoadKey(string _KeyA, string _KeyB, string Mode)//装载密码
        {
            stopwatch.Start();
            int k = 0;
            try
            {
                ClearCardInfo(Card_Info);
                if (_KeyB == "" || _KeyA == "")
                {
                    Card_Info.Message = "请输入密码";
                }
                else
                {
                    if (!StringVerdict(_KeyA) || !StringVerdict(_KeyB))
                    {
                        Card_Info.Message = "输入密码格式有误！";
                    }
                    else
                    {
                        byte[] keyA = Key(_KeyA);
                        byte[] keyB = Key(_KeyB);
                        Load_Key[1] = 0x15;
                        Load_Key[3] = 0x2B;

                        for (int i = 0; i < 6; i++)
                        {
                            Load_Key[4 + i] = keyA[i];
                            Load_Key[10 + i] = keyB[i];
                        }
                        if (Mode == "默认模式")
                        {
                            Load_Key[16] = 0x00;
                        }
                        else
                        {
                            Load_Key[16] = 0x55;
                        }
                        Load_Key[17] = 0x03;
                        Load_Key[18] = 0x08;
                        Load_Key[19] = 0x05;
                        Load_Key[20] = 0x02;
                        Load_Key[21] = 0x07;

                        Load_Key[22] = checkSum(Load_Key, 1, 21);
                        byte flag7FNum = check7F(Load_Key, 1, 22);
                        if (flag7FNum != 0)
                        {
                            int N = Load_Key.Length + flag7FNum;
                            bufferTemporary.Add(Load_Key[0]);
                            for (int j = 1; j < Load_Key.Length; j++)
                            {
                                bufferTemporary.Add(Load_Key[j]);
                                if (Load_Key[j] == 0x7F)
                                {
                                    bufferTemporary.Add(Load_Key[j]);
                                }
                            }
                            serialport1.WriteByteToSerialPort(bufferTemporary.ToArray(), N);
                        }
                        else
                        {
                            serialport1.WriteByteToSerialPort(Load_Key, 23);

                        }
                        bufferTemporary.RemoveRange(0, bufferTemporary.Count);
                    }

                }
            }
            catch (Exception error)
            {
                //Card_Info.Message = error.Message;
                Card_Info.Message = "未知异常";
                Card_Info.Code = -3;
            }
            while (true)
            {
                Thread.Sleep(700);
                if (k >= 10 || Card_Info.Code != 0xFF)
                    break;
                k++;
            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }

        /// <summary>
        /// 读块数据，判断有无卡
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>

        public CardInfo CardVerdict(string CardID, Int32 SUM)//判断有没有卡
        {
            stopwatch.Start();
            int k = 0;
            try
            {
                ClearCardInfo(Card_Info);
               
                serialport1.WriteByteToSerialPort(ReadBlock , 6);
            
            }
            catch (Exception erro)
            {
                // Card_Info.Message = erro.Message;
                Card_Info.Message = "未知异常";
                Card_Info.Code = -3;
            }
            while (true)
            {
                Thread.Sleep(700);
                if (k >= 10 || Card_Info.Code != 0xFF)
                    break;
                k++;

            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;


        }
        byte checkSum(byte[] data, int offset, int length)//计算校验码
        {
            byte temp = 0;
            for (int i = offset; i < length + offset; i++)
            {
                temp ^= data[i];

            }
            return temp;
        }
        byte check7F(byte[] data, int offset, int length)//判断除帧头外是否存在0x7F,并返回其个数
        {
            byte temp = 0;
            for (int i = offset; i < data.Length ; i++)
            {
                if (data[i] == 0x7F)
                    temp++;
            }
            return temp;
        }
        void serialport1_ResceiveMessage(object sender, SerialRecieveEventArgs e)
        {
            //throw new NotImplementedException() ;
            stopwatch.Start();
           
            lock (_lock1)
            {
                try
                {
                    //Thread.Sleep(500);
                    buffer.AddRange(e.receiveData);
                    //semaSerialPort.WaitOne();
                    dataverdict(buffer);
                    ComRec();
                    //this.Invoke(interfaceUpdateHandle);
                    // semaSerialPort.Release();

                }
                catch
                {
                    buffer.RemoveRange(0, buffer.Count);
                    bufferReceveData.RemoveRange(0, bufferReceveData.Count);
                }
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            WriteLOG(e.receiveData  , "接收的数据", ts.ToString());
            
        }
        private void dataverdict(List<byte> buffer)//对接收到的数据做判断
        {
            try
            {
                while (buffer.Count >= 3)
                {
                    byte head = buffer[0];
                    byte DataCount = buffer[1];
                    byte function_code = buffer[3];
                    byte []ByteTemp=buffer .ToArray ();
                    

                    if (head == 0x7F && DataCount == buffer.Count - 2 )
                    {
                        myQ.Enqueue(buffer);
                        //buffer.RemoveRange(0, buffer.Count);
                        break;
                    }
                    else if (head == 0x7F && DataCount !=buffer.Count - 2) 
                    {
                        bufferReceveData.Add(ByteTemp[0]);
                        for (int p = 1; p < ByteTemp.Length; p++)
                        {
                            bufferReceveData.Add(ByteTemp[p]);
                            if (buffer[p] == 0x7F)
                            {
                                p++;
                            }
                        }
                        if (DataCount == bufferReceveData .Count-2)
                        {
                            myQ.Enqueue(bufferReceveData);
                            break;
                        }
                        break;
                    }
                    else
                    {
                        buffer.RemoveRange(0, buffer.Count);
                        break;
                    }                       
                }
            }
            catch
            {
                buffer.RemoveRange(0, buffer.Count);

            }
        }
        void ComRec()//协议解析
        {

            if (myQ.Count > 0)
            {

                byte[] recBuffer = (myQ.Dequeue() as List<byte>).ToArray();
                string[] str1 = new string[recBuffer.Length];
                int len = recBuffer.Length;
                byte CRC;
                byte FunCode = recBuffer[3];
                int CardID = 0;
                byte state = recBuffer[4];
                CRC = checkSum(recBuffer, 1, len - 2);
                buffer.RemoveRange(0, buffer.Count);   //清缓存
                bufferReceveData.RemoveRange(0, bufferReceveData.Count);
                if (CRC == recBuffer[len - 1])
                {
                    if (FunCode == 0x90)//读卡
                    {
                        switch (state)
                        {
                            case 0x00:
                                System.Media.SystemSounds.Hand.Play();
                                //CardID = Convert.ToInt32(recBuffer[10] << 24 | recBuffer[9] << 16 | recBuffer[8] << 8 | recBuffer[7]);
                                Card_Info.Card_no = Byte4_UInt32_nixu(recBuffer, 10).ToString();
                                Jine = recBuffer[11] << 24 | recBuffer[12] << 16 | recBuffer[13] << 8 | recBuffer[14];
                                break;
                            case 0xFF:
                                Jine = 0xFF;
                                break;
                            case 0xFE:
                                Jine = 0xFE;
                                break;
                            case 0xFB:
                                Jine = 0xFB;

                                break;
                        }


                    }
                    if (FunCode == 0x93)//办卡
                    {
                        WriteLOG(recBuffer,"办卡", ts.ToString());
                        switch (state)
                        {
                            case 0x00:            //正确
                                System.Media.SystemSounds.Hand.Play();
                                Card_Info.Code = 0;
                                //int ID1 = Convert.ToInt32(recBuffer[10] << 24 | recBuffer[9] << 16 | recBuffer[8] << 8 | recBuffer[7]);
                                Card_Info.Card_no = Byte4_UInt32_nixu(recBuffer, 10).ToString();
                                Card_Info.Message = "操作成功";

                                NewCard_flag = true;
                                break;
                            case 0xFF:            //无卡
                                Jine = 0xFF;
                                Card_Info.Code = -1;
                                break;
                            case 0xFE:             //错误
                                Jine = 0xFE;
                                Card_Info.Code = -2;
                                break;
                            case 0xFB:             //校验错误
                                Jine = 0xFB;
                                Card_Info.Code = -2;
                                break;
                        }

                    }
                    if (FunCode == 0x95)//充值
                    {
                        WriteLOG(recBuffer, "充值", ts.ToString());
                        switch (state)
                        {
                            case 0x00:
                                System.Media.SystemSounds.Hand.Play();
                                Card_Info.Code = 0;
                                int Cash1 = Convert.ToInt32(recBuffer[11] << 24 | recBuffer[12] << 16 | recBuffer[13] << 8 | recBuffer[14] - 255) / 256 - 1000;
                                UInt32  Cash = (Byte4_UInt32(recBuffer, 11) - 255) / 256 - 1000;
                                //Card_Info.Card_no = ID2.ToString();

                                Card_Info.Card_no = Byte4_UInt32_nixu(recBuffer, 10).ToString();
                                Card_Info.Money =Cash; 
                                Card_Info.Message = "操作成功";

                                break;
                            case 0xFF:
                                Jine = 0xFF;       //无卡
                                Card_Info.Code = -1;
                                break;
                            case 0xFE:
                                Jine = 0xFE;        //错误
                                Card_Info.Code = -2;
                                break;
                            case 0xFB:              //校验错误
                                Jine = 0xFB;
                                Card_Info.Code = -2;
                                break;
                        }

                    }
                    if (FunCode == 0x96)//刷卡
                    {
                        WriteLOG(recBuffer, "刷卡", ts.ToString());
                        int ID3 = 0;
                        switch (state)
                        {
                            case 0x00:
                                System.Media.SystemSounds.Hand.Play();
                                Card_Info.Code = 0;
                                
                                int mid1 = BitConverter.ToInt32(recBuffer, 11);
                                int mid2 = Convert.ToInt32(recBuffer[11] << 24 | recBuffer[12] << 16 | recBuffer[13] << 8 | recBuffer[14]);
                                int Cash1 = Convert.ToInt32(recBuffer[11] << 24 | recBuffer[12] << 16 | recBuffer[13] << 8 | recBuffer[14] - 255) / 256 - 1000;
                                UInt32 Cash = (Byte4_UInt32(recBuffer, 11) - 255) / 256 - 1000;
                                Card_Info.Card_no = Byte4_UInt32_nixu(recBuffer, 10).ToString();
                                Card_Info.Money = Cash;
                                Card_Info.Message = "操作成功";

                                break;
                            case 0xFF:
                                Jine = 0xFF;         //无卡
                                Card_Info.Code = -1;
                                break;
                            case 0xFE:
                                Jine = 0xFE;         //错误
                                Card_Info.Code = -2;
                                break;
                            case 0xFB:
                                Jine = 0xFB;       //校验错误
                                Card_Info.Code = -2;
                                break;
                            case 0xFC:
                                Jine = 0xFC;       //扣款时余额不足
                                Card_Info.Code = -4;
                                break;
                        }
                    }
                    if (FunCode == 0x94)//恢复卡
                    {
                        WriteLOG(recBuffer, "恢复卡", ts.ToString());
                        switch (state)
                        {
                            case 0x00:
                                System.Media.SystemSounds.Hand.Play();
                                Card_Info.Code = 0;
                                //int ID4 = Convert.ToInt32(recBuffer[10] << 24 | recBuffer[9] << 16 | recBuffer[8] << 8 | recBuffer[7]);
                                Card_Info.Card_no = Byte4_UInt32_nixu(recBuffer, 10).ToString();
                                Card_Info.Message = "操作成功";

                                break;
                            case 0xFF:                  //无卡
                                Jine = 0xFF;
                                Card_Info.Code = -1;
                                break;
                            case 0xFE:                   //错误
                                Jine = 0xFE;
                                Card_Info.Code = -2;
                                break;
                            case 0xFB:
                                Jine = 0xFB;            //校验错误
                                Card_Info.Code = -2;
                                break;
                        }
                    }

                    if (FunCode == 0xAB)//装载密码
                    {
                        WriteLOG(recBuffer, "装载密码", ts.ToString());

                        switch (state)
                        {
                            case 0x00:
                                System.Media.SystemSounds.Hand.Play();
                                Card_Info.Code = 0;
                                Card_Info.Message = "操作成功";
                                break;
                            case 0xFF:
                                Card_Info.Code = -1;

                                break;
                            case 0xFE:
                                Card_Info.Code = -2;
                                break;
                            case 0xFB:
                                Card_Info.Code = -2;
                                break;
                        }
                    }
                    if (FunCode == 0xAC)//设置波特率
                    {
                        WriteLOG(recBuffer, "设置波特率", ts.ToString());
                        switch (state)
                        {
                            case 0x00:
                                System.Media.SystemSounds.Hand.Play();
                                Card_Info.Code = 0;
                                Card_Info.Message = "操作成功";
                                break;
                            case 0xFF:
                                Card_Info.Code = -1;
                                break;
                            case 0xFE:
                                Card_Info.Code = -2;
                                break;
                            case 0xFB:
                                Card_Info.Code = -2;
                                break;
                        }
                    }
                    if (FunCode == 0x91)//判断卡
                    {
                        WriteLOG(recBuffer, "判断卡是否存在", ts.ToString());
                        switch (state)
                        {
                            case 0x00:
                                System.Media.SystemSounds.Hand.Play();
                                Card_Info.Code = 0;
                                Card_Info.Message = "有卡";
                                break;
                            case 0xFF:
                                Card_Info.Code = -1;
                                Card_Info.Message = "无卡";
                                break;
                            case 0xFE:
                                Card_Info.Code = -2;
                                Card_Info.Message = "无卡";
                                break;
                            case 0xFB:
                                Card_Info.Code = -2;
                                Card_Info.Message = "无卡";
                                break;
                        }
                        
                    }
                }

            }
        }

        /// <summary>
        /// 数据类型转换，字节数组转为十进制数
        /// </summary>
        /// <param name="CD"></param>
        /// 

        UInt32 Byte4_UInt32(byte [] array,int StartIndex)
        {
            string str = null ;
            for (int t = 0; t < 4; t++)
            {
                string str1;
                str1 = Convert.ToString(array[t+StartIndex ], 16).ToUpper();
                str1 = ((str1.Length == 1 ? "0" + str1 : str1));
                str += str1;
                Console.WriteLine("十六进制字符串{0}", t);
                Console.WriteLine(str1);
            }
            UInt32 result = UInt32.Parse(str, System.Globalization.NumberStyles.HexNumber);
            return result;
        }
        UInt32 Byte4_UInt32_nixu(byte[] array, int StartIndex)
        {
            string str = null;
            for (int t = 0; t >-4; t--)
            {
                string str1;
                str1 = Convert.ToString(array[t + StartIndex], 16).ToUpper();
                str1 = ((str1.Length == 1 ? "0" + str1 : str1));
                str += str1;
                Console.WriteLine("十六进制字符串{0}", t);
                Console.WriteLine(str1);
            }
            UInt32 result = UInt32.Parse(str, System.Globalization.NumberStyles.HexNumber);
            return result;
        }
        
        void ClearCardInfo(CardInfo CD)
        {
            CD.Card_no = string.Empty;
            CD.Code = 0xFF;
            CD.Money = 0;
            CD.Message = string.Empty;
        }
        public static byte[] intToBytes2(Int32 n)//
        {
            byte[] b = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                b[i] = (byte)(n >> (24 - i * 8));

            }
            return b;
        }
        bool StringVerdict(string str)
        {
            bool flag = false;
            if (str.Length != 12)
            {
                return false;
            }
            else
            {
                int k = 0;
                foreach (char _str in str)
                {
                    if (_str < '0' || _str > '9')
                        flag = false;
                    k++;
                }
                if (k >= 12)
                    flag = true;
            }
            return flag;
        }
        byte[] Key(string str)
        {
            byte[] bt = new byte[6] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            for (int i = 0; i < 6; i++)
            {
                string str1 = str.Substring(2 * i, 2);
                bt[i] = (byte)(Convert.ToInt32(str1, 16));
            }
            return bt;
        }
        /// <summary>
        /// 接收数据保存
        /// </summary>
        /// <param name="CD"></param>
        /// 
        void WriteLOG( byte [] array,string operation,string time)
        {
            int a = 0;
            try
            {
                DateTime dt = DateTime.Now;
                Console.WriteLine(a);
                using (FileStream fp = File.Open("Log.txt", FileMode.Create))
                {
                    StreamWriter sw = new StreamWriter(fp);
                    sw.Write(dt.ToString()); //  26/11/2009 AM 11:21:30
                    sw.Write("\r\n");
                    sw.Write("******************" + operation + "历时计算" + "************************");
                    sw.Write("\r\n");
                    sw.Write(time);
                    sw.Write("\r\n");
                    Console.WriteLine(dt.ToString());
                    sw.Write("\r\n");
                   //\t是转义字符，表示制表符，相当于键盘上的Tab键按一次的效果
                    for (int t = 0; t < array.Length;t++ )
                    {
                        string str1 = Convert.ToString(array[t], 16);
                        sw.Write(str1 ); 
                        sw.Write('\t');
                    }
                    sw.Write("\r\n");
                    sw.Write("\r\n");
                    sw.Write("\r\n");


                }
                //MessageBox.Show("保存成功");
            }
            catch
            {
                Console.WriteLine("Input data error!");
                //MessageBox.Show("Input data error!");
            }
        }
        void WriteTime(string operation,string time)
        {
           
            try
            {
                using (FileStream fp = File.Open("Log.txt", FileMode.Create))
                {
                    StreamWriter sw = new StreamWriter(fp);
                    sw.Write("\r\n");
                    sw.Write("******************" + operation +"历时计算"+ "************************");
                    sw.Write("\r\n");
                    sw.Write(time);
                    sw.Write("\r\n");

                }
                //MessageBox.Show("保存成功");
            }
            catch
            {
                Console.WriteLine("Input data error!");
                //MessageBox.Show("Input data error!");
            }
        }
        
    }
}
