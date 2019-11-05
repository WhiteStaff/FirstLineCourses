using System;
using System.Collections.Generic;
using System.Text;

namespace NotebookApp
{
    static class Companion
    {
        public static void Greetings()
        {
            Console.WriteLine("Приветствую, это записная книжка, сейчас я покажу тебе что я умею!");
        }

        public static void Info()
        {
            Console.WriteLine("Для создания новой записи напиши \"1\"");
            Console.WriteLine("Для поиска записей напиши \"2\"");
            Console.WriteLine("Для редактирования записей напиши \"3\"");
            Console.WriteLine("Для удаления записи напиши \"4\"");
            Console.WriteLine("Для отображения всех записей напиши \"5\"");
            Console.WriteLine("Для показа справки напиши \"справка\"");
            Console.WriteLine("Для выхода из программы напиши \"пока\" (Помни, я всё забуду если закроюсь!)");
        }
    }
}
