using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow.Generator;
using TechTalk.SpecFlow.Generator.UnitTestProvider;
using TechTalk.SpecFlow.Utils;

namespace NCrunch.Generator.SpecflowPlugin
{
    public class NCrunchAttributeGeneratorProvider : IUnitTestGeneratorProvider
    {
        private static readonly string NCrunchAttributePrefix = "NCrunch.Framework";
        private static readonly string NCrunchExclusivelyUses = "NCrunch.Framework.ExclusivelyUsesAttribute";
        private readonly IUnitTestGeneratorProvider baseGeneratorProvider;
        private readonly CodeDomHelper codeDomHelper;

        public NCrunchAttributeGeneratorProvider(CodeDomHelper codeDomHelper)
        {
            baseGeneratorProvider = new NUnitTestGeneratorProvider(codeDomHelper);
            this.codeDomHelper = codeDomHelper;
        }

        public void SetTestClass(TestClassGenerationContext generationContext, string featureTitle,
            string featureDescription)
        {
            baseGeneratorProvider.SetTestClass(generationContext, featureTitle, featureDescription);
        }

        public void SetTestClassCategories(TestClassGenerationContext generationContext,
            IEnumerable<string> featureCategories)
        {
            baseGeneratorProvider.SetTestClassCategories(generationContext, featureCategories);
        }

        public void SetTestClassIgnore(TestClassGenerationContext generationContext)
        {
            baseGeneratorProvider.SetTestClassIgnore(generationContext);
        }

        public void FinalizeTestClass(TestClassGenerationContext generationContext)
        {
            baseGeneratorProvider.FinalizeTestClass(generationContext);
        }

        public void SetTestClassInitializeMethod(TestClassGenerationContext generationContext)
        {
            baseGeneratorProvider.SetTestClassInitializeMethod(generationContext);
        }

        public void SetTestClassCleanupMethod(TestClassGenerationContext generationContext)
        {
            baseGeneratorProvider.SetTestClassCleanupMethod(generationContext);
        }

        public void SetTestInitializeMethod(TestClassGenerationContext generationContext)
        {
            baseGeneratorProvider.SetTestInitializeMethod(generationContext);
        }

        public void SetTestCleanupMethod(TestClassGenerationContext generationContext)
        {
            baseGeneratorProvider.SetTestCleanupMethod(generationContext);
        }

        public void SetTestMethod(TestClassGenerationContext generationContext, CodeMemberMethod testMethod,
            string scenarioTitle)
        {
            baseGeneratorProvider.SetTestMethod(generationContext, testMethod, scenarioTitle);
        }

        public void SetTestMethodCategories(TestClassGenerationContext generationContext, CodeMemberMethod testMethod,
            IEnumerable<string> scenarioCategories)
        {
            baseGeneratorProvider.SetTestMethodCategories(generationContext, testMethod,
                scenarioCategories.Where(category => !category.StartsWith(NCrunchAttributePrefix)));
            foreach (string nCrunchCategories in scenarioCategories.Where(c => c.StartsWith(NCrunchAttributePrefix)))
            {
                string[] split = nCrunchCategories.Split('_');

                string nCrunchAttributeIdentifier = split.First();

                if (nCrunchAttributeIdentifier == NCrunchExclusivelyUses)
                {
                    string nCrunchAttributeValue = split.Last();
                    codeDomHelper.AddAttribute(testMethod, NCrunchExclusivelyUses, nCrunchAttributeValue);
                }
            }
        }

        public void SetTestMethodIgnore(TestClassGenerationContext generationContext, CodeMemberMethod testMethod)
        {
            baseGeneratorProvider.SetTestMethodIgnore(generationContext, testMethod);
        }

        public void SetRowTest(TestClassGenerationContext generationContext, CodeMemberMethod testMethod,
            string scenarioTitle)
        {
            baseGeneratorProvider.SetRowTest(generationContext, testMethod, scenarioTitle);
        }

        public void SetRow(TestClassGenerationContext generationContext, CodeMemberMethod testMethod,
            IEnumerable<string> arguments,
            IEnumerable<string> tags, bool isIgnored)
        {
            baseGeneratorProvider.SetRow(generationContext, testMethod, arguments, tags, isIgnored);
        }

        public void SetTestMethodAsRow(TestClassGenerationContext generationContext, CodeMemberMethod testMethod,
            string scenarioTitle,
            string exampleSetName, string variantName, IEnumerable<KeyValuePair<string, string>> arguments)
        {
            baseGeneratorProvider.SetTestMethodAsRow(generationContext, testMethod, scenarioTitle, exampleSetName,
                variantName, arguments);
        }

        public bool SupportsRowTests
        {
            get { return baseGeneratorProvider.SupportsRowTests; }
        }

        public bool SupportsAsyncTests
        {
            get { return baseGeneratorProvider.SupportsAsyncTests; }
        }
    }
}