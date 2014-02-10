using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace NCrunch.Framework
// ReSharper restore CheckNamespace
{
    public abstract class ResourceUsageAttribute : Attribute
    {
        private readonly string[] resourceNames;

        public ResourceUsageAttribute(params string[] resourceName)
        {
            resourceNames = resourceName;
        }

        public string[] ResourceNames
        {
            get { return resourceNames; }
        }
    }

    public class ExclusivelyUsesAttribute : ResourceUsageAttribute
    {
        public ExclusivelyUsesAttribute(params string[] resourceName)
            : base(resourceName) { }
    }

    public class InclusivelyUsesAttribute : ResourceUsageAttribute
    {
        public InclusivelyUsesAttribute(params string[] resourceName)
            : base(resourceName) { }
    }

    public class IsolatedAttribute : Attribute
    {
    }

    namespace NCrunch.Framework
    {
        public class SerialAttribute : Attribute
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Assembly, AllowMultiple = true)]
    public class RequiresCapabilityAttribute : Attribute
    {
        public RequiresCapabilityAttribute(string capabilityName)
        {
            CapabilityName = capabilityName;
        }

        public string CapabilityName { get; private set; }
    }

    public class TimeoutAttribute : System.Attribute
    {
        private IDictionary properties;

        public TimeoutAttribute(int timeout)
        {
            properties = new Hashtable();
            properties["Timeout"] = timeout;
        }

        public IDictionary Properties
        {
            get { return properties; }
        }
    }

    [AttributeUsage(AttributeTargets.Method
		| AttributeTargets.Class
		| AttributeTargets.Field
		| AttributeTargets.Assembly,
		AllowMultiple = true)]
	public class CategoryAttribute: Attribute
	{
		public CategoryAttribute(string category)
		{
			Category = category;
		}

		public string Category { get; private set; }
	}
}