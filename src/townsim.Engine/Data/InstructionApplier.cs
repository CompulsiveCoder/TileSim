using System;
using townsim.Entities;
using System.Reflection;

namespace townsim.Data
{
	public class InstructionApplier
	{
		public InstructionApplier ()
		{
		}

		public void Apply(BaseGameEntity target, EditInstruction instruction)
		{
			var type = target.GetType ();

			var property = type.GetProperty (instruction.TargetProperty);

			if (property == null)
				throw new MissingMemberException (type.FullName, instruction.TargetProperty);

			property.SetValue (target, CastPropertyValue(property, instruction.NewValue));
		}

		public object CastPropertyValue(PropertyInfo property, object value) { 
			// http://stackoverflow.com/questions/907882/cast-a-property-to-its-actual-type-dynamically-using-reflection
			if (property == null || value == null)
				return null;
			if (property.PropertyType.IsEnum)
			{
				Type enumType = property.PropertyType;
				if (Enum.IsDefined(enumType, value))
					return Enum.Parse(enumType, value.ToString());
			}
			if (property.PropertyType == typeof(bool))
				return value == "1" || value == "true" || value == "on" || value == "checked";
			else if (property.PropertyType == typeof(Uri))
				return new Uri(Convert.ToString(value));
			else
				return Convert.ChangeType(value, property.PropertyType); 
		}
	}
}

