using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, Inherited = false)]
public class BindableAttribute : Attribute
{ }