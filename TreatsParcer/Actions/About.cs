﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml.Schema;
using TreatsParcer.Actions.Interfaces;

namespace TreatsParcer.Actions
{
    class About : MenuAction
    {
        public string Category => "About";
        public string Name => "О программе";

        public void Perform()
        {
            MessageBox.Show("Программа для парсинга базы угроз РФ. \nВерсия 1.0 \n" +
                            "Внимание!! Поддержа программы заканчивается 31.12.2019", Name);
        }
    }
}