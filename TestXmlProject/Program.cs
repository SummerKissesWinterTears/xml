using System;
using System.Linq;
using System.Xml.Linq;

namespace XmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //Загрузка xml файла. Файл services лежит в папке bin\Debug
            XDocument doc = XDocument.Load("services.xml");

            //Отбор по <item></item> нужных нам сервисов
            var services = from r in doc.Descendants("item")
                           select new
                           {
                               //В данном случае все что находится между <type></type>
                               Item = r.Element("type").Value,
                           };
            //Вывод             
            foreach (var r in services)
            {
                Console.WriteLine(r.Item);
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Console.WriteLine();
            Console.WriteLine("Введите интересующий сервис, из списка выше. Чтобы выйти напечатайте exit");

            //Берём в качестве переменной, что введём в консоль
            string inputservice = "";

            //Выполняем опрос пользователя, пока он не напишет "exit"
            do
            {
                string inputservice2 = Console.ReadLine();

                if (inputservice2 != "exit")
                {
                    //Отбор по <item></item> и <type></type> нужных нам wsdl 
                    var selectedService2 = from r in doc.Descendants("item").Where(r => inputservice2 == r.Element("type").Value)
                                           select new
                                           {
                                               wsdl = r.Element("wsdl").Value,
                                           };

                    //Вывод об ошибке
                    if (selectedService2.Any() != false)
                    {
                        foreach (var r in selectedService2)
                        {
                            Console.WriteLine(r.wsdl);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неправильный сервис, наберите из данного списка выше");
                    }
                }
                else
                {
                    inputservice = "exit";
                }
            }
            while (inputservice != "exit");
        }
    }
}

