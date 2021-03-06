﻿using System.Globalization;
using Compost.Reflection;
using NUnit.Framework;

namespace Compost.Tests.Reflection
{
    public class ReflectorTests
    {
        [TestFixture]
        public class WithInstance
        {
            private const string FOO = "asdf";

            private static readonly TestClassAlpha TestClass = new TestClassAlpha();

            [Test]
            public void member_info_returns_info_for_fields_and_properties()
            {
                Assert.That(Reflector.MemberInfo(() => TestClass.SomeField).Name, Is.EqualTo("SomeField"));
                Assert.That(Reflector.MemberInfo(() => TestClass.SomeProperty).Name, Is.EqualTo("SomeProperty"));
            }

            [Test]
            public void member_info_throws_if_not_a_member()
            {
                AssertThat.ExceptionIsThrown<NotAMemberException>(
                    () => Reflector.MemberInfo(() => TestClass.ReturnSomething()));
            }

            [Test]
            public void member_info_throws_if_given_a_constant()
            {
                AssertThat.ExceptionIsThrown<NotAMemberException>(
                    () => Reflector.MemberInfo(() => FOO));
            }

            [Test]
            public void member_info_returns_info_for_unary_expressions()
            {
                var d = 0;

                Assert.That(Reflector.MemberInfo(() => -d).Name, Is.EqualTo("d"));
            }

            [Test]
            public void member_name_returns_name()
            {
                Assert.That(Reflector.MemberName(() => TestClass.SomeField), Is.EqualTo("SomeField"));
                Assert.That(Reflector.MemberName(() => TestClass.SomeProperty), Is.EqualTo("SomeProperty"));
            }

            [Test]
            public void method_info_returns_info_for_method()
            {
                Assert.That(Reflector.MethodInfo(() => TestClass.ReturnSomething()).Name, Is.EqualTo("ReturnSomething"));
                Assert.That(Reflector.MethodInfo(() => TestClass.DoSomething()).Name, Is.EqualTo("DoSomething"));
            }

            [Test]
            public void method_info_throws_if_not_a_method()
            {
                AssertThat.ExceptionIsThrown<NotAMethodException>(() => Reflector.MethodInfo(() => TestClass.SomeField));
            }

            [Test]
            public void method_name_returns_name()
            {
                Assert.That(Reflector.MethodName(() => TestClass.ReturnSomething()), Is.EqualTo("ReturnSomething"));
                Assert.That(Reflector.MethodName(() => TestClass.DoSomething()), Is.EqualTo("DoSomething"));
            }

            [Test]
            public void property_info_returns_info()
            {
                Assert.AreEqual(typeof (string), Reflector.PropertyInfo(() => TestClass.SomeProperty).PropertyType);
            }

            [Test]
            public void property_info_throws_if_not_a_property()
            {
                var testing = (1 + 2).ToString(CultureInfo.InvariantCulture);

                AssertThat.ExceptionIsThrown<NotAPropertyException>(() => Reflector.PropertyInfo(() => TestClass.SomeField));
                AssertThat.ExceptionIsThrown<NotAPropertyException>(() => Reflector.PropertyInfo(() => testing));
            }

            [Test]
            public void field_info_returns_info()
            {
                Assert.AreEqual(typeof (int), Reflector.FieldInfo(() => TestClass.SomeField).FieldType);
            }

            [Test]
            public void field_info_throws_if_not_a_field()
            {
                AssertThat.ExceptionIsThrown<NotAFieldException>(() => Reflector.FieldInfo(() => TestClass.SomeProperty));
            }
        }

        [TestFixture]
        public class WithoutInstance
        {
            [Test]
            public void member_info_returns_info_for_fields_and_properties()
            {
                Assert.That(Reflector.MemberInfo((TestClassAlpha testClass) => testClass.SomeField).Name,
                    Is.EqualTo("SomeField"));
                Assert.That(Reflector.MemberInfo((TestClassAlpha testClass) => testClass.SomeProperty).Name,
                    Is.EqualTo("SomeProperty"));
            }

            [Test]
            public void member_info_throws_if_not_a_member()
            {
                AssertThat.ExceptionIsThrown<NotAMemberException>(
                    () => Reflector.MemberInfo((TestClassAlpha testClass) => testClass.ReturnSomething()));
            }

            [Test]
            public void member_name_returns_name()
            {
                Assert.That(Reflector.MemberName((TestClassAlpha testClass) => testClass.SomeField),
                    Is.EqualTo("SomeField"));
                Assert.That(Reflector.MemberName((TestClassAlpha testClass) => testClass.SomeProperty),
                    Is.EqualTo("SomeProperty"));
            }

            [Test]
            public void method_info_returns_info_for_method()
            {
                Assert.That(Reflector.MethodInfo((TestClassAlpha testClass) => testClass.ReturnSomething()).Name,
                    Is.EqualTo("ReturnSomething"));
                Assert.That(Reflector.MethodInfo((TestClassAlpha testClass) => testClass.DoSomething()).Name,
                    Is.EqualTo("DoSomething"));
            }

            [Test]
            public void method_info_throws_if_not_a_method()
            {
                AssertThat.ExceptionIsThrown<NotAMethodException>(
                    () => Reflector.MethodInfo((TestClassAlpha testClass) => testClass.SomeField));
            }

            [Test]
            public void method_name_returns_name()
            {
                Assert.That(Reflector.MethodName((TestClassAlpha testClass) => testClass.ReturnSomething()),
                    Is.EqualTo("ReturnSomething"));
                Assert.That(Reflector.MethodName((TestClassAlpha testClass) => testClass.DoSomething()),
                    Is.EqualTo("DoSomething"));
            }

            [Test]
            public void get_enum_values_returns_all_values()
            {
                var vals = Reflector.GetEnumValues<TestEnum>();

                Assert.That(vals.Length, Is.EqualTo(6));
                Assert.That(vals, Contains.Item(TestEnum.An));
                Assert.That(vals, Contains.Item(TestEnum.O));
                Assert.That(vals, Contains.Item(TestEnum.This));
                Assert.That(vals, Contains.Item(TestEnum.Neighbor));
                Assert.That(vals, Contains.Item(TestEnum.Howdy));
                Assert.That(vals, Contains.Item(TestEnum.Is));
            }

            [Test]
            public void property_info_returns_info()
            {
                Assert.AreEqual(typeof (string), Reflector.PropertyInfo((TestClassAlpha testClass) => testClass.SomeProperty).PropertyType);
            }

            [Test]
            public void property_info_throws_if_not_a_property()
            {
                var testing = (1 + 2).ToString(CultureInfo.InvariantCulture);

                AssertThat.ExceptionIsThrown<NotAPropertyException>(
                    () => Reflector.PropertyInfo((TestClassAlpha testClass) => testClass.SomeField));
                AssertThat.ExceptionIsThrown<NotAPropertyException>(
                    () => Reflector.PropertyInfo((TestClassAlpha testClass) => testing));
            }

            [Test]
            public void field_info_returns_info()
            {
                Assert.AreEqual(typeof (int), Reflector.FieldInfo((TestClassAlpha testClass) => testClass.SomeField).FieldType);
            }

            [Test]
            public void field_info_throws_if_not_a_field()
            {
                AssertThat.ExceptionIsThrown<NotAFieldException>(
                    () => Reflector.FieldInfo((TestClassAlpha testClass) => testClass.SomeProperty));
            }
        }

        private class TestClassAlpha
        {
            public int SomeField;
            public string SomeProperty { get; set; }
            public void DoSomething() {}

            public double ReturnSomething()
            {
                return -1;
            }
        }

        private enum TestEnum
        {
            Howdy,
            Neighbor,
            This,
            Is,
            An,
            O
        }
    }
}