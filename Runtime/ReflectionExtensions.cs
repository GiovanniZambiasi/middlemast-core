using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MiddleMast
{
    public static class ReflectionExtensions
    {
        public static FieldInfo GetFieldInParent(this Type type, string name, BindingFlags flags)
        {
            Type currentType = type;
            FieldInfo field;
            int sanity = 0;

            do
            {
                field = currentType.GetField(name, flags);
                currentType = currentType.BaseType;
                sanity++;

                if (sanity > 16)
                {
                    break;
                }
            }
            while (field == null);

            return field;
        }

        public static bool HasAttribute(this MemberInfo member, Type attribute)
        {
            IEnumerable<CustomAttributeData> attributes = member.CustomAttributes;

            for (int i = 0; i < attributes.Count(); i++)
            {
                CustomAttributeData customAttributeData = attributes.ElementAt(i);

                if (customAttributeData.AttributeType == attribute)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasAttribute<TAttribute>(this MemberInfo member)
            where TAttribute : Attribute
        {
            return member.HasAttribute(typeof(TAttribute));
        }

        public static bool IsEnumerable(this Type type)
        {
            Type enumerableType = typeof(IEnumerable);

            return enumerableType.IsAssignableFrom(type);
        }

        public static T GetAttribute<T>(this MemberInfo member)
            where T : Attribute
        {
            IEnumerable<CustomAttributeData> attributes = member.CustomAttributes;
            Type type = typeof(T);
            Attribute customAttribute = Attribute.GetCustomAttribute(member, type);

            return customAttribute != null ? customAttribute as T : null;
        }

        public static void GetAllConcreteInheritors(this Type type, IEnumerable<Assembly> assembliesToSweep, List<Type> inheritors)
        {
            for (int i = 0; i < assembliesToSweep.Count(); i++)
            {
                Assembly assembly = assembliesToSweep.ElementAt(i);
                type.GetAllConcreteInheritors(assembly, inheritors);
            }
        }

        public static void GetAllConcreteInheritors(this Type type, Assembly assembly, List<Type> inheritors)
        {
            Type[] types = assembly.GetTypes();

            for (int i = 0; i < types.Length; i++)
            {
                Type element = types[i];

                if (element.IsClass && !element.IsAbstract && element.IsSubclassOf(type))
                {
                    inheritors.Add(element);
                }
            }
        }

        public static void GetAllConcreteTypesThatImplement(this Type type, IEnumerable<Assembly> assemblies, List<Type> implementing)
        {
            for (int i = 0; i < assemblies.Count(); i++)
            {
                Assembly assembly = assemblies.ElementAt(i);
                type.GetAllConcreteTypesThatImplement(assembly, implementing);
            }
        }

        public static void GetAllConcreteTypesThatImplement(this Type type, Assembly assembly, List<Type> implementing)
        {
            Type[] types = assembly.GetTypes();

            for (int i = 0; i < types.Length; i++)
            {
                Type element = types[i];

                if (element.IsClass && !element.IsAbstract && type.IsAssignableFrom(element))
                {
                    implementing.Add(element);
                }
            }
        }
    }
}
