using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityEditor.XCodeEditorChartboost
{
	public class PBXList : ArrayList
	{
		public PBXList()
		{
			
		}
		
		public PBXList( object firstvalueCurrent)
		{
			this.Add( firstvalueCurrent);
		}
	}
	
//	public class PBXList<T> : ArrayList
//	{
//		public int Add( T valueCurrent)
//		{
//			return (ArrayList)this.Add( valueCurrent);
//		}
//	}
}
