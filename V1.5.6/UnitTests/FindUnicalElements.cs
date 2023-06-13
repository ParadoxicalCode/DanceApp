using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class FindUnicalElements
    {
        [Fact]
        public void Test1()
        {
            var allAgeCategories = new string[2, 10]
            {
                { "1"   , "1", "2"   , "2"   , "3", "3"   , "3", "4", "5"   , "6" },
                { "соло", "" , "соло", "соло", "" , "соло", "" , "" , "соло", ""  }
            };
            var ageCategories = new string[2, 10];

            ageCategories[0, 0] = allAgeCategories[0, 0];
            ageCategories[1, 0] = allAgeCategories[1, 0];
            int a = 1;
            for (int x = 1; x < 10; x++)
            {
                bool result = false;
                for (int j = 0; j < 10; j++)
                {
                    bool first = false;
                    bool second = false;
                    if (allAgeCategories[0, x] == ageCategories[0, j] && ageCategories[0, j] != null)
                        first = true;
                    if (allAgeCategories[1, x] == ageCategories[1, j] && ageCategories[0, j] != null)
                        second = true;
                    if (first == true && second == true)
                    {
                        result = true;
                        break;
                    }
                }
                if (result == false)
                {
                    ageCategories[0, a] = allAgeCategories[0, x];
                    ageCategories[1, a] = allAgeCategories[1, x];
                    a++;
                }
                // Берём первый элемент и записываем его в массив. Берём следующий элемент, если его нет в новом массиве, то записываем
            }


            var expected = new string[2, 10]
            {
                { "1"   , "1", "2"   , "3", "3"   , "4", "5"   , "6", null, null },
                { "соло", "" , "соло", "" , "соло", "" , "соло", "" , null, null }
            };

            Assert.Equal(ageCategories, expected);
        }
    }
}
