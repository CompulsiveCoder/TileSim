using System;

namespace townsim.Entities
{
	public class EditInstruction : BaseInstruction
	{
		public string TargetProperty;

		public object NewValue;

		public EditInstruction (
			Type targetType,
			string targetId,
			string targetProperty,
			object newValue
		)
		{
			TargetType = targetType;
			TargetId = targetId;
			TargetProperty = targetProperty;
			NewValue = newValue;
		}
	}
}

