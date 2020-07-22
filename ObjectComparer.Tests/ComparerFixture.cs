using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectComparer.Tests
{
    [TestClass]
    public class ComparerFixture
    {

        [TestMethod]
        public void Class_values_are_similar_test()
        {
            var a = new Student
            {
                Name = "John",
                Id = 100,
                Marks = new[] { 80, 90, 100 },
                Subjects = new List<Subject>
                {
                    new Subject { Code = "001", Name ="Subject 1" },
                    new Subject { Code = "002", Name ="Subject 2" }

                }
            };

            var b = new Student
            {
                Name = "John",
                Id = 100,
                Marks = new[] { 80, 90, 100 },
                Subjects = new List<Subject>
                {
                    new Subject { Code = "002", Name ="Subject 2" },
                    new Subject { Code = "001", Name ="Subject 1" }
                }
            };

            var c = new Student
            {
                Name = "John",
                Id = 101,
                Marks = new[] { 80, 90, 100 },
                Subjects = new List<Subject>
                {
                    new Subject { Code = "001", Name ="Subject 1" },
                    new Subject { Code = "002", Name ="Subject 2" }
                }
            };

            var d = new Student
            {
                Name = "John",
                Id = 100,
                Marks = new[] { 100, 90, 80 },
                Subjects = new List<Subject>
                {
                    new Subject { Code = "001", Name ="Subject 1" },
                    new Subject { Code = "002", Name ="Subject 2" }
                }
            };

            Assert.IsTrue(Comparer.AreSimilar(a, b));
            Assert.IsFalse(Comparer.AreSimilar(a, c));
            Assert.IsTrue(Comparer.AreSimilar(a, d));


        }

        [TestMethod]
        public void Primitive_values_are_similar_test()
        {
            Assert.IsTrue(Comparer.AreSimilar(1, 1));
            Assert.IsFalse(Comparer.AreSimilar(1, 2));
            Assert.IsTrue(Comparer.AreSimilar(1, 1.0));
            Assert.IsTrue(Comparer.AreSimilar("John", "John"));
        }

        [TestMethod]
        public void Class_with_primitive_type_members_are_similar_test()
        {
            var x = new PrimitiveMembersClass
            {
                Prop1 = 1,
                Prop2 = "One"
            };
            var y = new PrimitiveMembersClass
            {
                Prop1 = 1,
                Prop2 = "One"
            };
            var z = new PrimitiveMembersClass
            {
                Prop1 = 1,
                Prop2 = "2One"
            };
            Assert.IsTrue(Comparer.AreSimilar(x, y));
            Assert.IsFalse(Comparer.AreSimilar(y, z));
        }

        [TestMethod]
        public void Class_with_anon_class_type_members_are_similar_test()
        {
            var x = new
            {
                Prop1 = 1,
                Prop2 = "One"
            };
            var y = new
            {
                Prop1 = 1,
                Prop2 = "One"
            };
            var z = new
            {
                Prop1 = 1,
                Prop2 = "2One"
            };
            Assert.IsTrue(Comparer.AreSimilar(x, y));
            Assert.IsFalse(Comparer.AreSimilar(y, z));
        }

        [TestMethod]
        public void Primitive_array_are_similar_test()
        {
            Assert.IsTrue(Comparer.AreSimilar(new int[] { 1, 2, 3, 4 }, new int[] { 2, 1, 3, 4 }));
            Assert.IsFalse(Comparer.AreSimilar(new int[] { 1, 2, 3, 4 }, new int[] { 1, 7, 3, 4 }));
            Assert.IsTrue(Comparer.AreSimilar(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'c', 'b' }));
            Assert.IsFalse(Comparer.AreSimilar(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'd', 'c' }));
            Assert.IsTrue(Comparer.AreSimilar(new string[] { "one", "two", "three" }, new string[] { "three", "two", "one" }));
            Assert.IsFalse(Comparer.AreSimilar(new string[] { "one", "two", "three" }, new string[] { "three", "two", "zero" }));
        }

        [TestMethod]
        public void Primitive_list_are_similar_test()
        {
            Assert.IsTrue(Comparer.AreSimilar(new List<int> { 1, 2, 3, 4 }, new List<int> { 2, 1, 3, 4 }));
            Assert.IsFalse(Comparer.AreSimilar(new List<int> { 1, 2, 3, 4 }, new List<int> { 1, 7, 3, 4 }));
            Assert.IsTrue(Comparer.AreSimilar(new List<char> { 'a', 'b', 'c' }, new List<char> { 'a', 'c', 'b' }));
            Assert.IsFalse(Comparer.AreSimilar(new List<char> { 'a', 'b', 'c' }, new List<char> { 'a', 'd', 'c' }));
            Assert.IsTrue(Comparer.AreSimilar(new List<string> { "one", "two", "three" }, new List<string> { "three", "two", "one" }));
            Assert.IsFalse(Comparer.AreSimilar(new List<string> { "one", "two", "three" }, new List<string> { "three", "two", "zero" }));
        }

        [TestMethod]
        public void Primitive_dictionary_are_similar_test()
        {
            IDictionary<int, string> d1 = new Dictionary<int, string>()
                                            {
                                                {1,"One"},
                                                {2, "Two"},
                                                {3,"Three"}
                                            };
            IDictionary<int, string> d2 = new Dictionary<int, string>()
                                            {
                                                {1,"One"},
                                                {2, "Two"},
                                                {3,"Three"}
                                            };
            IDictionary<int, string> d3 = new Dictionary<int, string>()
                                            {
                                                {1,"One"},
                                                {2, "Two"},
                                                {3,"four"}
                                            };
            Assert.IsTrue(Comparer.AreSimilar(d1, d2));
            Assert.IsFalse(Comparer.AreSimilar(d1, d3));
        }

    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] Marks { get; set; }

        public List<Subject> Subjects { get; set; }
    }

    class Subject
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    class PrimitiveMembersClass
    {
        public int Prop1 { get; set; }
        public string Prop2 { get; set; }
    }

}
