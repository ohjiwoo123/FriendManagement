﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FriendManagement
{
    delegate void Function();
    enum GenderType { NON, 남성, 여성 }
    static public class MyLibrary
    {
        #region 콘솔 처리 기능
        public static void ConsoleClear()
        {
            Console.Clear();
        }
        public static void ConsolePause()
        {
            Console.WriteLine("\n아무 키나 누르세요");
            Console.ReadKey();
        }
        #endregion

        #region 입력기능
        public static string InputString(string msg)
        {
            Console.Write(msg + " : ");
            return Console.ReadLine();
        }

        public static int InputInteger(string msg)
        {
            Console.Write(msg + " : ");
            return int.Parse(Console.ReadLine());
        }
        #endregion
    }

    class Friend
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public GenderType Gender { get; private set; }

        #region 생성자
        public Friend(string _name, int _age, GenderType _gender)
        {
            Name = _name;
            Age = _age;
            Gender = _gender;
        }
        #endregion

        #region 메서드
        public void Print()
        {
            Console.Write("[" + Gender + "]" + Name + "\t\t" + Age + "세\n");
        }
        public void Println()
        {
            Console.WriteLine("[성	 별]" + Gender);
            Console.WriteLine("[이	 름]" + Name);
            Console.WriteLine("[학   과]" + Age);
        }
        #endregion
    }

    
    internal class Program {
        static List<Friend> ar = new List<Friend>(10);
        private static ConsoleKey menuprint()
        {
            Console.WriteLine("******************************************************");
            Console.WriteLine(" [F1] 저장(Insert)\n");
            Console.WriteLine(" [F2] 검색(Select)\n");
            Console.WriteLine(" [F3] 수정(Update)\n");
            Console.WriteLine(" [F4] 삭제(Delete)\n");
            Console.WriteLine(" [F5] 저장(Save)\n");
            Console.WriteLine(" [F6] 불러오기(Load)\n");
            Console.WriteLine(" [ESC] 프로그램종료\n");
            Console.WriteLine("******************************************************");

            ConsoleKey key = Console.ReadKey().Key;
            Console.Write("\b");
            return key;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                MyLibrary.ConsoleClear();

                PrintAll();

                // Non Delegate

                //switch (menuprint())
                //{
                //    case ConsoleKey.F1: Insert(); break;
                //    case ConsoleKey.F2: Select(); break;
                //    case ConsoleKey.F3: Update(); break;
                //    case ConsoleKey.F4: Delete(); break;
                //    case ConsoleKey.F5: Save(); break;
                //    case ConsoleKey.F6: Load(); break;
                //    case ConsoleKey.Escape: return;
                //}

                // Using Delegate
                Function[] func = { Insert, Select, Update, Delete, Save, Load };
                int choice = menuprint() - ConsoleKey.F1;
                if(choice < 0 || choice >5 )
                {
                    Console.WriteLine("F1~F6 사이의 값만 입력해주세요");
                    Thread.Sleep(1000);
                    continue;
                }
                else
                {
                    func[choice]();
                }
                MyLibrary.ConsolePause();
            }
        }
        
        static public void PrintAll()
        {
            Console.WriteLine("Print All");
            foreach (var x in ar)
            {
                x.Print();
            }
        }
        static public void Insert()
        {
            Console.WriteLine("Insert");
            string name = MyLibrary.InputString("이름을 입력하세요");
            int age = MyLibrary.InputInteger("나이를 입력하세요");
            GenderType gender = (GenderType)MyLibrary.InputInteger("성별을 번호로 입력하세요 (0=NULL, 1=남자, 2=여자)");
            Friend friend = new Friend(name,age,gender);
            ar.Add(friend);
        }
        static public void Select()
        {
            Console.WriteLine("Select");
            // 검색한 이름과 (내가 콘솔에 입력한 이름과)
            // 저장되어 있는 이름이 같다면 그 결과 출력
            string name = Console.ReadLine();
            foreach(var x in ar)
            {
                if (x.Name == name)
                {
                    x.Print();
                    break;
                }
            }
        }
        static public void Update()
        {
            Console.WriteLine("Update");
            string name = MyLibrary.InputString("이름을 입력하세요");
            for (int i = 0; i < ar.Count; i++)
            {
                if (ar[i].Name == name)
                {
                    Console.WriteLine("수정 사항을 입력하세요.");
                    string updateName = MyLibrary.InputString("이름을 입력하세요");
                    int updateAge = MyLibrary.InputInteger("나이를 입력하세요");
                    GenderType updateGender = (GenderType)MyLibrary.InputInteger("성별을 입력하세요");
                    Friend friend = new Friend(updateName, updateAge, updateGender);
                    ar[i]= new Friend(updateName, updateAge,updateGender);
                }
            }
        }

        static public void Delete()
        {
            Console.WriteLine("Delete");
            string name = Console.ReadLine();
            foreach (var x in ar)
            {
                if (x.Name == name)
                {
                    ar.Remove(x);
                    break;
                }
            }
        }
        static public void Save()
        {
            Console.WriteLine("Save");
            FileStream fs = new FileStream(@"c:\Temp\AddrBook2.dat",   // file path
                                                FileMode.CreateNew,    // file mode
                                                FileAccess.Write);  // file access

            BinaryWriter bw = new BinaryWriter(fs);

            int Total = ar.Count();

            bw.Write(Total);

            for (int i=0; i<Total;i++)
            {
                bw.Write(ar[i].Name);
                bw.Write(ar[i].Age);
                bw.Write((int)ar[i].Gender);
            }

            for(int j=Total; j==0; j--)
            {
                ar.RemoveAt(j);
            }

            fs.Close();
            bw.Close();
        }

        static public void Load()
        {
            Console.WriteLine("Load");
            FileStream fs = new FileStream(@"c:\Temp\AddrBook2.dat",   // file path
                                                FileMode.Open,    // file mode
                                                FileAccess.Read);  // file access
            BinaryReader br = new BinaryReader(fs);

            int Total = br.ReadInt32();

            for (int i=0; i<Total; i++)
            {
                string name = br.ReadString();
                int age = br.ReadInt32();
                GenderType gender = (GenderType)br.ReadInt32();
                Friend friend = new Friend(name, age, gender);
                ar.Add(friend);
            }
        }
    }
}
