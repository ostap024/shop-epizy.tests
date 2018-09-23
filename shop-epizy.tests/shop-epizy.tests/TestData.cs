using CsvHelper;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace shop_epizy.tests
{
    public class TestData
    {

        public static IEnumerable GetTestData
        {
            get
            {
                using (CsvReader csv = new CsvReader(new StreamReader(@"C:\Users\ostap\source\repos\shop-epizy.tests\shop-epizy.tests\shop-epizy.tests\bin\Debug\test-data.csv"), true))
                {
                    while (csv.Read())
                    {
                        string email = csv.GetField(0);
                        string currPassword = csv.GetField(1);
                        string newPassword = csv.GetField(2);
                        yield return new TestCaseData(email, currPassword, newPassword);
                    }
                }
            }
        }
    }

}
