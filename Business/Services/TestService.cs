using Project.Provider;
using Project.Provider.Exception;
using Business.Interfaces;
using DAL.Context;
using DAL.Entities.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Business.Services
{
    public class TestService : BaseProvider, ITestService
    {
        private readonly IConfiguration _configuration;
        private List<string> data = new List<string>();

        public TestService(ProjectContext db, IConfiguration configuration) : base(db)
        {
            _configuration = configuration;
        }

        public List<Person> GetPerson()
        {
            var persons = new List<Person>();

            persons.Add(new Person
            {
                Id = 1,
                FirstName = "faa",
                LastName = "laa"
            });

            persons.Add(new Person
            {
                Id = 2,
                FirstName = "faa 2",
                LastName = "laa 2"
            });

            persons.Add(new Person
            {
                Id = 3,
                FirstName = "faa 3",
                LastName = "laa 4"
            });

            //var persons = _db.Persons.ToList();
            //if (persons is null)
            //{
            //    throw new PersonException.NotFound();
            //}

            return persons!;
        }

        public async Task<Person> GetPerson(int id)
        {
            var persons = _db.Persons.SingleOrDefault(a => a.Id == id);
            if (persons is null)
            {
                throw new PersonException.NotFound(id);
            }

            return persons!;
        }


        public async Task<List<string>> Test2(string inputText)
        {
            var responseText = new List<string>();

            char[] arr = inputText.ToCharArray();

            int x = arr.Length - 1;
            CheckText(arr, 0, x);

            responseText = data.Distinct().ToList();

            return responseText!;
        }

        private void CheckText(char[] list, int k, int m)
        {
            if (k == m)
            {
                data.Add("[" + string.Join(", ", list) + "]");
            }
            else
            {
                for (int i = k; i <= m; i++)
                {
                    SwapText(ref list[k], ref list[i]);
                    CheckText(list, k + 1, m);
                    SwapText(ref list[k], ref list[i]);
                }

            }

        }

        private void SwapText(ref char a, ref char b)
        {
            if (a != b)
            {
                var temp = a;
                a = b;
                b = temp;
            };
        }
        
        public async Task<string> Test3(string inputNumber)
        {
            var responseText = 0;

            var arr = inputNumber.ToCharArray();

            if(arr.Length > 1)
            {
                foreach (var numberText in arr)
                {
                    try
                    {
                        var number = Convert.ToInt32(numberText.ToString());
                        if (number >= 2)
                        {
                            if (number % 2 == 0)
                            {
                                responseText = number > responseText ? number : responseText;
                            }
                            else
                            {
                                if (responseText != 0 && responseText % 2 != 0)
                                {
                                    responseText = number;
                                }
                            }
                        }
                        else
                        {
                            var newText = number == 0 ? 0 : (number == 1 && number > 2 ? number : 0);
                            responseText = newText > responseText ? newText : responseText;
                        }
                        
                    }
                    catch { }

                }
            }
            else
            {
                try { var number = Convert.ToInt32(arr[0].ToString()); 
                    responseText = number;
                }
                catch { }
            }
            
            return responseText.ToString();

        }

        public async Task<string> Test4(List<string> inputText)
        {
            //[
            //    ":)", ";(" , ";}", ":-D"
            //]
            //[
            //    ";D", ":-(", ":-)", ";~)"
            //]

            var textSmile = new List<string> { ":-)", ":-D", ":~)", ":~D", ";-)", ";-D", ";~)", ";~D", ":)", ":D", ":)", ":D", ";)", ";D", ";)", ";D" };

            var countSmile = 0;

            foreach (var itemText in inputText)
            {
                if (textSmile.Contains(itemText))
                {
                    countSmile++;
                }
            }

            return countSmile.ToString();

        }



    }
}
