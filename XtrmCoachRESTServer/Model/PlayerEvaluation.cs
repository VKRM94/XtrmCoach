using System;
using System.Collections.Generic;

namespace XtrmCoachRESTServer.Models
{
	public class PlayerEvaluation
	{
		public long id { get; set; }
		public long sportId { get; set; }
		public long playerId { get; set; }
		public PerformanceParameterName perfParaName { get; set; }
		public string customName { get; set; }
		public List<PerformanceParameterType> perfParaTypes { get; set; }
		public PerformanceParameterType selectedType { get; set; }
		public string value { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			PlayerEvaluation s = obj as PlayerEvaluation;
			if ((System.Object)s == null)
			{
				return false;
			}

			if (this.id != s.id) return false;
			if (this.sportId != s.sportId) return false;
			if (this.playerId != s.playerId) return false;
			if (this.perfParaName.id != s.perfParaName.id) return false;
			if (this.perfParaName.name != s.perfParaName.name) return false;
			if (this.perfParaTypes == null && s.perfParaTypes != null)
			{
				return false;
			}
			else if (this.perfParaTypes != null && s.perfParaTypes == null)
			{
				return false;
			}
			else if (this.perfParaTypes != null && s.perfParaTypes != null)
			{
				for (int perfParaTypeIndex = 0; perfParaTypeIndex < this.perfParaTypes.Count; perfParaTypeIndex++)
				{
					if (this.perfParaTypes[perfParaTypeIndex].id != s.perfParaTypes[perfParaTypeIndex].id) return false;
					if (this.perfParaTypes[perfParaTypeIndex].name != s.perfParaTypes[perfParaTypeIndex].name) return false;
				}
			}
			if (this.customName != s.customName) return false;
			if (this.selectedType.id != s.selectedType.id) return false;
			if (this.selectedType.groupId != s.selectedType.groupId) return false;
			if (this.selectedType.name != s.selectedType.name) return false;
			if (this.value != s.value) return false;

			return true;
		}
	}
}