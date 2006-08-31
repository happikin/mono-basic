// UtilsTest.cs - NUnit Test Cases for Microsoft.VisualBasic.CompilerServices.Utils 
//
// Boris Kirzner <borisk@mainsoft.com>
//
// 

// Copyright (c) 2002-2006 Mainsoft Corporation.
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using NUnit.Framework;
using System;
using System.IO;
using Microsoft.VisualBasic;

namespace MonoTests.Microsoft_VisualBasic.CompilerServices
{
	[TestFixture]
	public class UtilsTests 
	{
		public UtilsTests()
		{
		}

		[SetUp]
		public void GetReady() 
		{
		}

		[TearDown]
		public void Clean() 
		{
		}

		[Test]
		public void ReDimPreserve_SingleDimension() 
		{
			string[] a = new string[6];

			a[1] = "a";
			a[2] = "b";
			a[3] = "c";

			string[] b = (string[]) Microsoft.VisualBasic.CompilerServices.Utils.CopyArray((Array) a, new string[4]);

			// Assert.AreEqual(b[0],null);
			Assert.AreEqual(b[1],"a");
			Assert.AreEqual(b[2],"b");
			Assert.AreEqual(b[3],"c");
		}

		[Test]
		public void ReDimPreserve_MultipleDimensions() 
		{
			Assert.Fail("Test for ReDim multiple-dimensioned array is not implemented yet");
		}
	}
}