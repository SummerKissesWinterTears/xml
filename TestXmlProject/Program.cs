using System;
using System.Linq;
using System.Xml.Linq;

namespace XmlParser
{
    class Initialization
    {
        // Download an xml file. File "services" is in - bin\Debug
        XDocument doc = XDocument.Load("services.xml");
        public void logic()
        {   
            // Select from <item></item> to get elements we need(<type></type>) Linq
            var services = from element in doc.Descendants("item")
                           select new
                           {
                               // In this case we looking for <type></type>
                               Item = element.Element("type").Value,
                           };
            // Output            
            foreach (var element in services)
            {
                Console.WriteLine(element.Item);
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Console.WriteLine();
            Console.WriteLine("Введите интересующий сервис, из списка выше. Чтобы выйти напечатайте exit");

            // It runs until user types "exit"
            do
            {
                string inputservice2 = Console.ReadLine();

                if (!"exit".Equals(inputservice2))
                {
                    // Select from <item></item> and <type></type> to get(<wsdl></wsdl>) wsdl 
                    var selectedService2 = from r in doc.Descendants("item").Where(r => inputservice2 == r.Element("type").Value)
                                           select new
                                           {
                                               wsdl = r.Element("wsdl").Value,
                                           };

                    // An error message
                    if (!selectedService2.Any().Equals(false))
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
                    break;
                }
            }
            while (true);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Initialization init = new Initialization();
            init.logic();
        }
    }
}

