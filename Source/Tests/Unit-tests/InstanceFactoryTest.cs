using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegionOrebroLan.DependencyInjection.UnitTests
{
	[TestClass]
	public class InstanceFactoryTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Create_IfTheTypeParameterIsAnInvalidType_ShouldThrowAnInvalidOperationException()
		{
			var type = Guid.NewGuid().ToString();

			try
			{
				new InstanceFactory().Create(type);
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(this.IsValidException(invalidOperationException, type))
					throw;
			}
		}

		[TestMethod]
		[SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase")]
		public void Create_IfTheTypeParameterIsAValidType_ShouldReturnAnInstanceOfThatType()
		{
			var type = typeof(InstanceFactory);

			var typeValue = type.AssemblyQualifiedName;
			var instance = new InstanceFactory().Create(typeValue);
			Assert.IsInstanceOfType(instance, type);

			// Should work even if it is not a full assembly-qualified name.
			typeValue = $"{type.FullName}, {type.Assembly.GetName().Name}";
			instance = new InstanceFactory().Create(typeValue);
			Assert.IsInstanceOfType(instance, type);

			// Should work even if it is not a full assembly-qualified name and upper-case.
			typeValue = typeValue.ToUpperInvariant();
			instance = new InstanceFactory().Create(typeValue);
			Assert.IsInstanceOfType(instance, type);

			// Should work even if it is not a full assembly-qualified name and lower-case.
			typeValue = typeValue.ToLowerInvariant();
			instance = new InstanceFactory().Create(typeValue);
			Assert.IsInstanceOfType(instance, type);

			// Should work even if it is not a full assembly-qualified name and there is no whitespace after the comma.
			typeValue = $"{type.FullName},{type.Assembly.GetName().Name}";
			instance = new InstanceFactory().Create(typeValue);
			Assert.IsInstanceOfType(instance, type);

			// Should work even if it is not a full assembly-qualified name and there is no whitespace after the comma and upper-case.
			typeValue = typeValue.ToUpperInvariant();
			instance = new InstanceFactory().Create(typeValue);
			Assert.IsInstanceOfType(instance, type);

			// Should work even if it is not a full assembly-qualified name and there is no whitespace after the comma and lower-case.
			typeValue = typeValue.ToLowerInvariant();
			instance = new InstanceFactory().Create(typeValue);
			Assert.IsInstanceOfType(instance, type);

			// Should work even if it is not a full assembly-qualified name and the comma is immediately after the fullname.
			typeValue = $"   {type.FullName},   {type.Assembly.GetName().Name}   ";
			instance = new InstanceFactory().Create(typeValue);
			Assert.IsInstanceOfType(instance, type);

			// Should work even if it is not a full assembly-qualified name and the comma is immediately after the fullname, upper-case.
			typeValue = typeValue.ToUpperInvariant();
			instance = new InstanceFactory().Create(typeValue);
			Assert.IsInstanceOfType(instance, type);

			// Should work even if it is not a full assembly-qualified name and the comma is immediately after the fullname, lover-case.
			typeValue = typeValue.ToLowerInvariant();
			instance = new InstanceFactory().Create(typeValue);
			Assert.IsInstanceOfType(instance, type);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Create_IfTheTypeParameterIsEmpty_ShouldThrowAnInvalidOperationException()
		{
			var type = string.Empty;

			try
			{
				new InstanceFactory().Create(type);
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(this.IsValidException(invalidOperationException, type))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Create_IfTheTypeParameterIsNull_ShouldThrowAnInvalidOperationException()
		{
			const string type = null;

			try
			{
				new InstanceFactory().Create(type);
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(this.IsValidException(invalidOperationException, type))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Create_IfTheTypeParameterIsWhitespace_ShouldThrowAnInvalidOperationException()
		{
			const string type = " ";

			try
			{
				new InstanceFactory().Create(type);
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(this.IsValidException(invalidOperationException, type))
					throw;
			}
		}

		protected internal virtual bool IsValidException(InvalidOperationException invalidOperationException, string type)
		{
			if(invalidOperationException == null)
				return false;

			var typeAsFormatArgument = type != null ? $"\"{type}\"" : "NULL";

			if(!string.Equals(invalidOperationException.Message, $"Could not create an instance from type {typeAsFormatArgument}.", StringComparison.Ordinal))
				return false;

			// ReSharper disable ConvertIfStatementToReturnStatement
			if(!string.Equals(invalidOperationException.InnerException?.Message, $"Could not resolve the type from string-representation {typeAsFormatArgument}.", StringComparison.Ordinal))
				return false;
			// ReSharper restore ConvertIfStatementToReturnStatement

			return true;
		}

		#endregion
	}
}