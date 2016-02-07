using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Flubar.SimpleInjector.CodeGeneration
{
    public class CodeDomTest
    {
        static ISet<string> usings = new HashSet<string>();

        public static string Run()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace("Samples");
            compileUnit.Namespaces.Add(samples);
            samples.Imports.Add(new CodeNamespaceImport("System"));

            CodeTypeDeclaration class1 = new CodeTypeDeclaration("Class1");
            samples.Types.Add(class1);

            //CodeEntryPointMethod start = new CodeEntryPointMethod();
            //CodeMethodInvokeExpression cs1 = new CodeMethodInvokeExpression(
            //    new CodeTypeReferenceExpression("System.Console"),
            //    "WriteLine", new CodePrimitiveExpression("Hello World!"));
            //start.Statements.Add(cs1);
            //class1.Members.Add(start);

            //var t = typeof(global::SimpleInjector.Container);
            var t = typeof(DemoClass);

            var field = new CodeMemberField(t, "_" + t.Name.ToLower());
            class1.Members.Add(field);

            CodeConstructor ctor = new CodeConstructor();
            class1.Members.Add(ctor);
            var p = new CodeParameterDeclarationExpression(t, t.Name.ToLower());
            ctor.Parameters.Add(p);
            var fieldReference = new CodeFieldReferenceExpression(null, field.Name);
            ctor.Statements.Add(new CodeAssignStatement(fieldReference, new CodeVariableReferenceExpression(p.Name)));


            foreach (var sourceMethod in t.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => !x.IsSpecialName)
                .Where(x => x.DeclaringType != typeof(object)))
            {


                var method = new CodeMemberMethod();
                class1.Members.Add(method);
                method.Name = sourceMethod.Name;
                method.Attributes = MemberAttributes.Public;
                if (sourceMethod.ReturnType != typeof(void))
                {
                    method.ReturnType = GetSafeCodeTypeReference(sourceMethod.ReturnType);
                }

                if (sourceMethod.IsGenericMethod)
                {
                    //method.Name += "Generic";
                    foreach (var arg in sourceMethod.GetGenericArguments())
                    {
                        var cp = new CodeTypeParameter(arg.Name);
                        method.TypeParameters.Add(cp);
                        var cons = arg.GetGenericParameterConstraints();
                        if (cons.Length > 0)
                        {
                            //cp.Constraints.Add(GetSafeCodeTypeReference(arg.BaseType));
                            if (arg.IsClass)
                            {
                                cp.Constraints.Add("class");
                            }
                            if (arg.IsValueType)
                            {
                                cp.Constraints.Add("struct");
                            }
                        }


                    }
                    //continue;
                }

                foreach (var sourceParameter in sourceMethod.GetParameters())
                {
                    //var c1param = new CodeTypeParameter("TAbc");
                    //method.TypeParameters.Add(c1param);
                    //var c2param = new CodeTypeParameter("TDef");
                    //method.TypeParameters.Add(c2param);
                    //c2param.Constraints.Add("TAbc");
                    p = new CodeParameterDeclarationExpression(GetSafeCodeTypeReference(sourceParameter.ParameterType), sourceParameter.Name);
                    method.Parameters.Add(p);
                }

                CodeMethodInvokeExpression cs1 = new CodeMethodInvokeExpression(
                    new CodeMethodReferenceExpression(fieldReference,
                        sourceMethod.Name,
                        sourceMethod.GetGenericArguments().Select(x => new CodeTypeReference(x.Name)).ToArray()),
                        sourceMethod.GetParameters().Select(sp => new CodeVariableReferenceExpression(sp.Name)).ToArray());
                if (sourceMethod.ReturnType != typeof(void))
                {
                    method.Statements.Add(new CodeMethodReturnStatement(cs1));
                }
                else
                {
                    method.Statements.Add(cs1);
                }
                //"WriteLine", new CodePrimitiveExpression("Hello World!"));


            }


            return GenerateCSharpCode(compileUnit);
        }

        private static CodeTypeReference GetSafeCodeTypeReference(Type type)
        {
            if (!type.IsGenericType)
            {
                return new CodeTypeReference(type);
            }

            string typeName = GetSafeTypeName(type);
            return new CodeTypeReference(typeName);
        }

        private static string GetSafeTypeName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.FullName;
            }

            var generic = type.GetGenericTypeDefinition();
            var sb = new StringBuilder();
            sb.Append(generic.FullName.Substring(0, generic.FullName.IndexOf('`')));
            sb.Append('<');

            int i = 0;
            foreach (var arg in type.GetGenericArguments())
            {
                if (i++ > 0)
                {
                    sb.Append(", ");
                }
                sb.Append(GetSafeTypeName(arg));
            }
            //Out(string.Join(", ", t.GetGenericArguments().Select(x => OutType(x)).ToArray()));
            sb.Append('>');
            return sb.ToString();
        }

        public static string GenerateCSharpCode(CodeCompileUnit compileunit)
        {
            // Generate the code with the C# code provider.
            CSharpCodeProvider provider = new CSharpCodeProvider();

            // Build the output file name.
            //string sourceFile;
            //if (provider.FileExtension[0] == '.')
            //{
            //    sourceFile = "HelloWorld" + provider.FileExtension;
            //}
            //else
            //{
            //    sourceFile = "HelloWorld." + provider.FileExtension;
            //}

            // Create a TextWriter to a StreamWriter to the output file.
            using (var sw = new StringWriter())
            //using (var sw = new StreamWriter(stream))
            {

                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");

                // Generate source code using the code provider.
                provider.GenerateCodeFromCompileUnit(compileunit, tw,
                    new CodeGeneratorOptions());

                // Close the output file.
                //tw.Close();
                return sw.ToString();
            }
        }
    }

    public class CodeGenerator
    {
        public static string GetSafeTypeName(Type type)
        {
            if (!type.IsGenericType)
            {
                return (type.IsGenericParameter) ? type.Name : type.FullName;
            }

            var generic = type.GetGenericTypeDefinition();
            var sb = new StringBuilder();
            sb.Append(generic.FullName.Substring(0, generic.FullName.IndexOf('`')));
            sb.Append('<');

            int i = 0;
            foreach (var arg in type.GetGenericArguments())
            {
                if (i++ > 0)
                {
                    sb.Append(", ");
                }
                sb.Append(GetSafeTypeName(arg));
            }
            //Out(string.Join(", ", t.GetGenericArguments().Select(x => OutType(x)).ToArray()));
            sb.Append('>');
            return sb.ToString();
        }

        public static IEnumerable<string> GetTypeParametersConstrants(MethodInfo method)
        {
            var parameterConstraints = new List<string>();
            foreach (var arg in method.GetGenericArguments())
            {
                var cons = arg.GetGenericParameterConstraints();
                var constraints = cons.Where(x => x != typeof(ValueType)).Select(x => GetSafeTypeName(x)).ToList();
                if ((arg.GenericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) == GenericParameterAttributes.ReferenceTypeConstraint)
                {
                    constraints.Add("class");
                }
                else if ((arg.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) == GenericParameterAttributes.NotNullableValueTypeConstraint)
                {
                    constraints.Add("struct");
                }
                if (constraints.Count != 0)
                {
                    parameterConstraints.Add(GetSafeTypeName(arg) + ": " + string.Join(", ", constraints.ToArray()));
                }
            }
            return parameterConstraints;
        }
        public static string GetTypeParameters(MethodInfo method)
        {
            return string.Join(", ", method.GetGenericArguments().Select(x => x.Name).ToArray());
           
        }
    }
}
