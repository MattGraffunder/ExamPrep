using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ExamPrep.Chapter_2
{
    static class CodeDomGenerator
    {
        internal static string BuildAssembly()
        {
            CodeCompileUnit cu = new CodeCompileUnit();

            CodeNamespace codeDomNameSpace = new CodeNamespace("HamSpace");
            codeDomNameSpace.Imports.Add(new CodeNamespaceImport("System"));

            CodeTypeDeclaration hamClass = new CodeTypeDeclaration("HamClass");

            CodeEntryPointMethod start = new CodeEntryPointMethod();

            CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Console"), "WriteLine", new CodePrimitiveExpression("Ham Time!"));
            CodeMethodInvokeExpression readKeyExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Console"), "ReadKey");

            cu.Namespaces.Add(codeDomNameSpace);
            codeDomNameSpace.Types.Add(hamClass);
            hamClass.Members.Add(start);
            start.Statements.Add(expression);
            start.Statements.Add(readKeyExpression);

            CodeDomProvider gen = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";

            using (StringWriter s = new StringWriter())
            {
                gen.GenerateCodeFromCompileUnit(cu, s, options);

                s.Flush();

                return s.ToString();
            }
        }

        public static void GenerateAndRunExe()
        {
            string code = BuildAssembly();

            CSharpCodeProvider c = new CSharpCodeProvider();

            CompilerParameters p = new CompilerParameters();
            p.ReferencedAssemblies.Add("System.dll");

            p.GenerateExecutable = true;

            p.OutputAssembly = "HamTime.exe";

            p.GenerateInMemory = false;

            CompilerResults results = c.CompileAssemblyFromSource(p, code);

            if (results.Errors.Count == 0)
            {
                System.Diagnostics.Process.Start(results.PathToAssembly);
            }
        }

        //Expression Trees
    }

    static class ExpressionTester
    {
        public static Func<int, int> AddThreeOrSquare(bool shouldSquare)
        {
            //If shouldSquare generate expression to square, otherwise Double

            ParameterExpression number = Expression.Parameter(typeof(int), "number");
            ParameterExpression result = Expression.Parameter(typeof(int), "result");

            BinaryExpression doubleNum = Expression.Add(number, Expression.Constant(3));
            BinaryExpression square = Expression.Multiply(number, number);

            BinaryExpression doMath = shouldSquare ? square : doubleNum;

            BlockExpression block = Expression.Block(
                new[] { result },
                Expression.Assign(result, doMath)
                );

            return Expression.Lambda<Func<int, int>>(block, number).Compile();
        }
    }
}