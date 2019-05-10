using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaintColorSelector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintColorSelector.Models.Tests
{
	[TestClass()]
	public class UtilityTests
	{
		[TestMethod()]
		public void ConvertHSLTest()
		{
			Assert.AreEqual((000, 000, 0xff), Utility.ConvertHSL(0xff, 0xff, 0xff));
			Assert.AreEqual((000, 000, 0x80), Utility.ConvertHSL(0x80, 0x80, 0x80));
			Assert.AreEqual((000, 000, 0x00), Utility.ConvertHSL(0x00, 0x00, 0x00));

			Assert.AreEqual((000, 255, 127), Utility.ConvertHSL(0xff, 0x00, 0x00));
			Assert.AreEqual((029, 255, 127), Utility.ConvertHSL(0xff, 0x7f, 0x00));
			Assert.AreEqual((060, 255, 127), Utility.ConvertHSL(0xff, 0xff, 0x00));
			Assert.AreEqual((091, 255, 127), Utility.ConvertHSL(0x7f, 0xff, 0x00));	//
			Assert.AreEqual((120, 255, 127), Utility.ConvertHSL(0x00, 0xff, 0x00));
			Assert.AreEqual((149, 255, 127), Utility.ConvertHSL(0x00, 0xff, 0x7f));
			Assert.AreEqual((180, 255, 127), Utility.ConvertHSL(0x00, 0xff, 0xff));
			Assert.AreEqual((211, 255, 127), Utility.ConvertHSL(0x00, 0x7f, 0xff));	//
			Assert.AreEqual((240, 255, 127), Utility.ConvertHSL(0x00, 0x00, 0xff));
			Assert.AreEqual((269, 255, 127), Utility.ConvertHSL(0x7f, 0x00, 0xff));
			Assert.AreEqual((300, 255, 127), Utility.ConvertHSL(0xff, 0x00, 0xff));
			Assert.AreEqual((331, 255, 127), Utility.ConvertHSL(0xff, 0x00, 0x7f));	//

		}
	}
}