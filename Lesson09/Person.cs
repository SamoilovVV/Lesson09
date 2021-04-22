using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson09
{
    class Person
    {
        public string Name { get; set; }

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                if (value < 0)
                {
                    throw new PersonException("Возраст не может быть отрицательным!");
                }

                _age = value;
            }
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            return $"{Name}, {Age}";
        }
    }
}
