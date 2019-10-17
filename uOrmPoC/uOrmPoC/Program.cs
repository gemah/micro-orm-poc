using Newtonsoft.Json;
using Nzr.Orm.Core;
using Nzr.Orm.Core.Sql;
using System;
using System.Collections.Generic;
using System.Xml;
using static Nzr.Orm.Core.Sql.Aggregate;
using static Nzr.Orm.Core.Sql.Alias;
using static Nzr.Orm.Core.Sql.Where;

namespace uOrmPoC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //const string connectionStrings = "Data Source=BLMG01DE-NTB007;Database=dummy;Integrated Security=true";
            Random rnd = new Random();
            IList<Nfe> nfes;

            // NF-e entity = { Id: int, Nome: string/nchar(50), Xml: String/Text }
            Nfe nfe = new Nfe()
            {
                Nome = "test",
                Xml = "<root><blive><local>Raja</local></blive></root>"
            };
            Options daoOptions = new Options()
            {
                NamingStyle = NamingStyle.LowerCase,
                UseComposedId = false,
                Schema = "dbo",
            };
            Dao dao = new Dao(new ConnectionManager(), daoOptions);

            using (dao)
            {
                // Insert and Select new NF-e
                dao.Insert(nfe);
                Nfe nfe2 = dao.Select<Nfe>(nfe.Id);

                Console.WriteLine("ID of inserted NF-e: " + nfe2.Id);
                Console.WriteLine("XML Content of inserted NF-e: " + nfe2.Xml.root.blive.local.ToString());

                ExNfe extra = new ExNfe()
                {
                    Nfe = nfe2,
                    Extra = "Extra: NF-e" + nfe2.Id
                };
                dao.Insert(extra);

                // Select with INNER JOIN
                IList<ExNfe> extras = dao.Select<ExNfe>(new Where());
                Console.WriteLine("NF-e (" + extras[0].Nfe.Id + "): " + extras[0].Id + " - " + extras[0].Extra);

                dao.Insert(new Nfe()
                {
                    Nome = "test2",
                    Xml = "<root><blive><local>Raja</local></blive></root>"
                });

                // Select all NF-es
                nfes = dao.Select<Nfe>(Where("Id", GE, 0));

                // Delete NF-e by entity
                int randomNfePosition = rnd.Next(nfes.Count);
                Nfe toDelete = nfes[randomNfePosition];
                int deletedLines = dao.Delete(toDelete);
                Console.WriteLine("NFe to delete - id:" + toDelete.Id + "\nDeleted Lines: " + deletedLines);

                // Delete NF-e by ID
                deletedLines = dao.Delete<Nfe>(Where("Id", EQ, randomNfePosition));
                Console.WriteLine("NFe to delete - id:" + nfes[randomNfePosition].Id + "\nDeleted Lines: " + deletedLines);

                // Update one NF-e randomly with new Name
                nfes = dao.Select<Nfe>(Where("Id", GE, 0));

                // Update by Entity
                randomNfePosition = rnd.Next(nfes.Count);
                Nfe toUpdate = nfes[randomNfePosition];

                toUpdate.Nome = "newNome";
                toUpdate.Xml = ConvertDynamicValue(toUpdate.Xml);
                int updatedLines = dao.Update(toUpdate);
                if (updatedLines > 0)
                {
                    Console.WriteLine("Update by Entity Successful");
                }

                // Update by Id
                updatedLines = dao.Update<Nfe>(Set("Nome", "otherNewName"), Where("Id", EQ, nfes[randomNfePosition].Id));
                if (updatedLines > 0)
                {
                    Console.WriteLine("Update by ID Successful");
                }

                // Aggregate COUNT where ids >= 20
                int result = dao.Aggregate<Nfe, int>(Aggregate(COUNT, "Id"), Where("Id", GE, 20));
                Console.WriteLine("Number of NF-es which Id is greater or equal than 20: " + result);

                // Aggregate AVG
                Console.WriteLine("Average of all NF-es Ids: " + dao.Aggregate<Nfe, double>(Aggregate(AVG, "Id")));

                // Aggregate SUM
                Console.WriteLine("Sum of all NF-es Ids: " + dao.Aggregate<Nfe, int>(Aggregate(SUM, "Id")));

                // Aggregate MAX
                Console.WriteLine("Max of all NF-es Ids: " + dao.Aggregate<Nfe, int>(Aggregate(MAX, "Id")));

                // Aggregate MIN
                Console.WriteLine("Min of all NF-es Ids: " + dao.Aggregate<Nfe, int>(Aggregate(MIN, "Id")));

                // Select with LEFT JOIN
                IList<LeftNfe> lefts = dao.Select<LeftNfe>(new Where());
                foreach (LeftNfe left in lefts)
                {
                    Console.WriteLine((left.Nfe.Id > 0) ? "NF-e <" + left.Nfe.Id + "> - Type: " + left.Type : "None found for Type: " + left.Type);
                }
            }
        }

        // JSON content to XML onversion logic
        private static string ConvertDynamicValue(dynamic rawValue)
        {
            string json = JsonConvert.SerializeObject(rawValue);
            XmlDocument xml = JsonConvert.DeserializeXmlNode(json);
            return xml.OuterXml;
        }

    }
}
