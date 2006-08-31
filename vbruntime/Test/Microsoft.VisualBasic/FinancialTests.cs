// FinancialTest.cs - NUnit Test Cases for Microsoft.VisualBasic.Financial
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
using System.Collections;
using Microsoft.VisualBasic;

namespace MonoTests.Microsoft_VisualBasic
{
	[TestFixture]
	public class FinancialTests
	{
		public FinancialTests()
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

		#region DDB Tests

		[Test]
		public void DDB_1()
		{
			Assert.AreEqual(47.999403820109578,Financial.DDB(1500,120,24,12,2.0));
			Assert.AreEqual(479.9940382010958,Financial.DDB(15000,1000,24,12,2.0));

			Assert.AreEqual(391.34749179845591,Financial.DDB(15000,1000,48,12,2.0));

			Assert.AreEqual(33.646996435384537,Financial.DDB(1500,120,24,12,4.0));

			Assert.AreEqual(43.160836714378092,Financial.DDB(1500,100,48,12,6.0));

			Assert.AreEqual(24.789790003786901,Financial.DDB(1500,100,48,12,1.0));

			Assert.AreEqual(383.10767441791506,Financial.DDB(15000,1000,48,12.5,2.0));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDB_2()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(1500,120,12,24,2.0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDB_3()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(1500,120,48,24,-1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDB_4()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(1500,-2,48,24,2.0);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException))]
		//LAMESPEC: MSDN claims the exception should be thrown in this case
		public void DDB_5()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(-5,120,48,24,2.0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDB_6()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(1500,120,48,-24,2.0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDB_7()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(1500,120,-2,-5,2.0);
		}

		#endregion

		#region SLN Tests

		[Test]
		public void SLN_1()
		{
			Assert.AreEqual(20.833333333333332,Financial.SLN(1500,500,48));
			Assert.AreEqual(10.416666666666666,Financial.SLN(1500,500,96));

			Assert.AreEqual(0,Financial.SLN(500,500,96));

			Assert.AreEqual(500,Financial.SLN(1500,500,2));
			Assert.AreEqual(-500,Financial.SLN(1500,500,-2));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SLN_2()
		{
			//Argument 'Life' cannot be zero.
			Financial.SLN(1500,500,0);
		}

		#endregion

		#region SYD Tests

		[Test]
		public void SYD_1()
		{
			Assert.AreEqual(44.047619047619044,Financial.SYD(1500,100,48,12));

			Assert.AreEqual(1.1904761904761905,Financial.SYD(1500,100,48,48));

			Assert.AreEqual(0,Financial.SYD(100,100,48,48));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYD_2()
		{
			// Argument 'Salvage' must be greater than or equal to zero.
			Financial.SYD(1500,-100,48,12);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYD_3()
		{
			// Argument 'Period' must be less than or equal to argument 'Life'.
			Financial.SYD(1500,100,8,12);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYD_4()
		{
			// Argument 'Period' must be greater than zero.
			Financial.SYD(1500,100,48,0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYD_5()
		{
			// Argument 'Period' must be greater than zero.
			Financial.SYD(1500,100,48,-2);
		}

		#endregion

		#region FV Tests

		[Test]
		public void FV_1()
		{
			Assert.AreEqual(-5042.6861628644065,Financial.FV(0.1/48,48,100,0,DueDate.EndOfPeriod));
			Assert.AreEqual(5042.6861628644065,Financial.FV(0.1/48,48,-100,0,DueDate.EndOfPeriod));

			Assert.AreEqual(6026.9653563801103,Financial.FV(0.45/48,48,-100,0,DueDate.EndOfPeriod));
			Assert.AreEqual(30134.826781900552,Financial.FV(0.45/48,48,-500,0,DueDate.EndOfPeriod));

			Assert.AreEqual(5053.1917590370413,Financial.FV(0.1/48,48,-100,0,DueDate.BegOfPeriod));


			Assert.AreEqual(1727.5182776853812,Financial.FV(0.1/48,48,-100,3000,DueDate.EndOfPeriod));
			Assert.AreEqual(8357.8540480434312,Financial.FV(0.1/48,48,-100,-3000,DueDate.EndOfPeriod));

			Assert.AreEqual(1738.023873858016,Financial.FV(0.1/48,48,-100,3000,DueDate.BegOfPeriod));
			Assert.AreEqual(8368.359644216067,Financial.FV(0.1/48,48,-100,-3000,DueDate.BegOfPeriod));

			Assert.AreEqual(-4572.3341785092407,Financial.FV(-0.1/48,48,100,0,DueDate.EndOfPeriod));

			Assert.AreEqual(-4599.4962842992118,Financial.FV(-0.1/48,48.3,100,0,DueDate.EndOfPeriod));
		}


		#endregion

		#region Rate Tests

		[Test]
		public void Rate_1()
		{
			Assert.AreEqual(-0.067958335561249847,Financial.Rate(48,-120,50000,0,DueDate.EndOfPeriod,0.1));

			Assert.AreEqual(-0.054284323350630818,Financial.Rate(48,-200,50000,0,DueDate.EndOfPeriod,0.1));

			Assert.AreEqual(-0.03391640485393424,Financial.Rate(48,-400,50000,0,DueDate.EndOfPeriod,0.1));

			Assert.AreEqual(0.19996831303445506,Financial.Rate(48,-1000,5000,0,DueDate.EndOfPeriod,0.1));

			Assert.AreEqual(-0.99999999998846933,Financial.Rate(48,-1000,5000,0,DueDate.BegOfPeriod,0.1));

			Assert.AreEqual(-0.055871364867281934,Financial.Rate(48,-200,50000,0,DueDate.BegOfPeriod,0.1));

			Assert.AreEqual(-0.065503055347169575,Financial.Rate(48,-200,50000,1000,DueDate.EndOfPeriod,0.1));

			Assert.AreEqual(-0.058920469572311909,Financial.Rate(48,-200,50000,500,DueDate.EndOfPeriod,0.1));
		}

		[Test]
		public void Rate_2()
		{
			Assert.AreEqual(-0.067958335561434935,Financial.Rate(48,-120,50000,0,DueDate.EndOfPeriod,0.3));

			Assert.AreEqual(-0.054284323350996831,Financial.Rate(48,-200,50000,0,DueDate.EndOfPeriod,0.3));

			Assert.AreEqual(-0.033916404853936467,Financial.Rate(48,-400,50000,0,DueDate.EndOfPeriod,0.3));

			Assert.AreEqual(0.19996831303445506,Financial.Rate(48,-1000,5000,0,DueDate.EndOfPeriod,0.2));

			Assert.AreEqual(-0.99999999999999079,Financial.Rate(48,-1000,5000,0,DueDate.BegOfPeriod,0.3));

			Assert.AreEqual(-0.055871364867277139,Financial.Rate(48,-200,50000,0,DueDate.BegOfPeriod,0.3));

			Assert.AreEqual(-0.065503055348340639,Financial.Rate(48,-200,50000,1000,DueDate.EndOfPeriod,0.3));

			Assert.AreEqual(-0.058920469572100169,Financial.Rate(48,-200,50000,500,DueDate.EndOfPeriod,0.3));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Rate_3()
		{
			// Argument 'NPer' must be greater than zero.
			Financial.Rate(0,-120,50000,0,DueDate.EndOfPeriod,0.1);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Rate_4()
		{
			// Argument 'NPer' must be greater than zero.
			Financial.Rate(-10,-120,50000,0,DueDate.EndOfPeriod,0.1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Rate_5()
		{
			// Cannot calculate rate using the arguments provided.
			Financial.Rate(48,-1000,5000,0,DueDate.EndOfPeriod,0.3);
		}

		#endregion

		#region IRR Tests

		[Test]
		public void IRR_1()
		{
			double[] values = new double[] {-50000, 20000, 20000, 20000, 10000};

			Assert.AreEqual(0.16479098450887533,Financial.IRR(ref values,0.1));

			Assert.AreEqual(0.16479098450893837,Financial.IRR(ref values,0.3));

			Assert.AreEqual(0.16479098450893415,Financial.IRR(ref values,0.5));

			values = new double[] {-100000, 40000, 35000, 30000, 25000};

			Assert.AreEqual(0.12441449540624081,Financial.IRR(ref values,0.1));

			Assert.AreEqual(0.12441449541502105,Financial.IRR(ref values,0.3));

			Assert.AreEqual(0.12441449541025705,Financial.IRR(ref values,0.5));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IRR_2()
		{
			//Arguments are not valid.
			double[] values = new double[] {-500000, 20, 20, 20, 10000};

			Financial.IRR(ref values,10000);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IRR_3()
		{
			//Arguments are not valid.
			double[] values = new double[] {100, 20, 30, 30, 30};

			Financial.IRR(ref values,0.1);
		}

		#endregion

		#region MIRR Tests

		[Test]
		public void MIRR_1()
		{
			double[] values = new double[] {-50000, 20000, 20000, 20000, 10000};

			Assert.AreEqual(0.14382317535283296,Financial.MIRR(ref values,0.1,0.12));

			Assert.AreEqual(0.32152371134887248,Financial.MIRR(ref values,0.3,0.5));

			Assert.AreEqual(0.41431961645993387,Financial.MIRR(ref values,0.5,0.7));

			values = new double[] {-100000, 40000, 35000, 30000, 25000};

			Assert.AreEqual(0.12239312521886214,Financial.MIRR(ref values,0.1,0.12));

			Assert.AreEqual(0.29787828889780776,Financial.MIRR(ref values,0.3,0.5));

			Assert.AreEqual(0.39034333083777972,Financial.MIRR(ref values,0.5,0.7));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MIRR_2()
		{
			// Argument 'FinanceRate' is not a valid value.
			double[] values = new double[] {-50000, 20000, 20000, 20000, 10000};

			Financial.MIRR(ref values,-1,0.12);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MIRR_3()
		{
			// Argument 'ReinvestRate' is not a valid value.
			double[] values = new double[] {-50000, 20000, 20000, 20000, 10000};

			Financial.MIRR(ref values,0.1,-1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MIRR_4()
		{
			// Argument 'ReinvestRate' is not a valid value.
			double[] values = new double[] {-50000, 20000, 20000, 20000, 10000};

			Financial.MIRR(ref values,0.1,-3);
		}

		[Test]
		[ExpectedException(typeof(System.NullReferenceException))]
		public void MIRR_5()
		{
			double[] values = null;

			Financial.MIRR(ref values,0.1,0.12);
		}

		[Test]
		[ExpectedException(typeof(DivideByZeroException))]
		public void MIRR_6()
		{
			double[] values = new double[] {100, 20, 30, 30, 30};

			Financial.MIRR(ref values,0.1,0.1);
		}

		#endregion

		#region NPer Tests

		[Test]
		public void NPer_1()
		{
			Assert.AreEqual(-47.613161535165531,Financial.NPer(0.1/48,200,10000,0,DueDate.EndOfPeriod));
			Assert.AreEqual(-47.518910769564712,Financial.NPer(0.1/48,200,10000,0,DueDate.BegOfPeriod));

			Assert.AreEqual(-401.12014892843246,Financial.NPer(0.1/48,200,10000,50000,DueDate.EndOfPeriod));
			Assert.AreEqual(-399.9412969471162,Financial.NPer(0.1/48,200,10000,50000,DueDate.BegOfPeriod));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPer_2()
		{
			// Argument 'Rate' is not a valid value.
			Financial.NPer(-1,200,10000,0,DueDate.EndOfPeriod);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPer_3()
		{
			// Argument 'Pmt' is not a valid value.
			Financial.NPer(0,0,10000,0,DueDate.EndOfPeriod);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPer_4()
		{
			// Cannot calculate number of periods using the arguments provided.
			Financial.NPer(0.1/48,0,10000,0,DueDate.EndOfPeriod);
		}

		#endregion

		#region IPmt Tests

		[Test]
		public void IPmt_1()
		{
			Assert.AreEqual(-29.246960945707116,Financial.IPmt(0.1/48, 36, 48, 50000, 0, DueDate.EndOfPeriod ));

			Assert.AreEqual(-55.550772957533624,Financial.IPmt(0.1/48, 24, 48, 50000, 0, DueDate.EndOfPeriod ));

			Assert.AreEqual(-2.2779661153231245,Financial.IPmt(0.1/48, 48, 48, 50000, 0, DueDate.EndOfPeriod ));

			Assert.AreEqual(-29.186156453096753,Financial.IPmt(0.1/48, 36, 48, 50000, 0, DueDate.BegOfPeriod ));

			Assert.AreEqual(-55.435282785064992,Financial.IPmt(0.1/48, 24, 48, 50000, 0, DueDate.BegOfPeriod ));

			Assert.AreEqual(-2.2732302190335001,Financial.IPmt(0.1/48, 48, 48, 50000, 0, DueDate.BegOfPeriod ));

			Assert.AreEqual(201.49943498736394,Financial.IPmt(0.1/48, 48, 48, 50000, 100000, DueDate.EndOfPeriod ));

			Assert.AreEqual(201.08051724310744,Financial.IPmt(0.1/48, 48, 48, 50000, 100000, DueDate.BegOfPeriod ));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IPmt_2()
		{
			// Argument 'Per' is not a valid value.
			Financial.IPmt(0.1/48, 56, 48, 50000, 0, DueDate.EndOfPeriod );
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IPmt_3()
		{
			// Argument 'Per' is not a valid value.
			Financial.IPmt(0.1/48, -48, -48, 50000, 0, DueDate.EndOfPeriod );
		}

		#endregion

		#region Pmt Tests

		[Test]
		public void Pmt_1()
		{
			Assert.AreEqual(-1095.7017014703874,Financial.Pmt(0.1/48, 48, 50000, 0, DueDate.EndOfPeriod ));

			Assert.AreEqual(-1093.4237353550641,Financial.Pmt(0.1/48, 48, 50000, 0, DueDate.BegOfPeriod ));

			Assert.AreEqual(-3078.7717710778288,Financial.Pmt(0.1/48, 48, 50000, 100000, DueDate.EndOfPeriod ));

			Assert.AreEqual(3182.938437744494,Financial.Pmt(0.1/48, -48, 50000, 100000, DueDate.EndOfPeriod ));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Pmt_2()
		{
			// Argument 'NPer' is not a valid value.
			Financial.Pmt(0.1/48, 0, 50000, 0, DueDate.EndOfPeriod );
		}

		#endregion

		#region PPmt Tests

		[Test]
		public void PPmt_1()
		{
			Assert.AreEqual(-1066.4547405246804,Financial.PPmt(0.1/48, 36, 48, 50000, 0, DueDate.EndOfPeriod ));

			Assert.AreEqual(-1040.1509285128539,Financial.PPmt(0.1/48, 24, 48, 50000, 0, DueDate.EndOfPeriod ));

			Assert.AreEqual(-1093.4237353550643,Financial.PPmt(0.1/48, 48, 48, 50000, 0, DueDate.EndOfPeriod ));

			Assert.AreEqual(-1064.2375789019673,Financial.PPmt(0.1/48, 36, 48, 50000, 0, DueDate.BegOfPeriod ));

			Assert.AreEqual(-1037.988452569999,Financial.PPmt(0.1/48, 24, 48, 50000, 0, DueDate.BegOfPeriod ));

			Assert.AreEqual(-1091.1505051360307,Financial.PPmt(0.1/48, 48, 48, 50000, 0, DueDate.BegOfPeriod ));

			Assert.AreEqual(-3280.2712060651929,Financial.PPmt(0.1/48, 48, 48, 50000, 100000, DueDate.EndOfPeriod ));

			Assert.AreEqual(-3273.4515154080918,Financial.PPmt(0.1/48, 48, 48, 50000, 100000, DueDate.BegOfPeriod ));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void PPmt_2()
		{
			// Argument 'Per' is not a valid value.
			Financial.PPmt(0.1/48, 56, 48, 50000, 0, DueDate.EndOfPeriod );
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void PPmt_3()
		{
			// Argument 'Per' is not a valid value.
			Financial.PPmt(0.1/48, -48, -48, 50000, 0, DueDate.EndOfPeriod );
		}

		#endregion

		#region NPV Tests

		[Test]
		public void NPV_1()
		{
			double[] values = new double[] {10000, 20000, 30000, 20000, 10000};

			Assert.AreEqual(68028.761075684073,Financial.NPV(0.1,ref values));

			Assert.AreEqual(42877.457964464716,Financial.NPV(0.3,ref values));

			Assert.AreEqual(22007.920515939288,Financial.NPV(0.7,ref values));

			Assert.AreEqual(15312.5,Financial.NPV(1,ref values));

			Assert.AreEqual(5895.8421600621214,Financial.NPV(2.3,ref values));

			Assert.AreEqual(-5203.7070453792549,Financial.NPV(-2.3,ref values));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPV_2()
		{
			//Argument 'Rate' is not a valid value.
			double[] values = new double[] {10000, 20000, 30000, 20000, 10000};

			Financial.NPV(-1,ref values);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPV_3()
		{
			// Argument 'ValueArray' is Nothing.
			double[] values = null;

			Financial.NPV(0.1,ref values);
		}

		#endregion

		#region PV Tests

		[Test]
		public void PV_1()
		{
			Assert.AreEqual(-4563.2857859855494,Financial.PV(0.1/48,48,100,0,DueDate.EndOfPeriod));
			Assert.AreEqual(4563.2857859855494,Financial.PV(0.1/48,48,-100,0,DueDate.EndOfPeriod));

			Assert.AreEqual(3851.0271688809689,Financial.PV(0.45/48,48,-100,0,DueDate.EndOfPeriod));
			Assert.AreEqual(19255.135844404842,Financial.PV(0.45/48,48,-500,0,DueDate.EndOfPeriod));

			Assert.AreEqual(4572.7926313730195,Financial.PV(0.1/48,48,-100,0,DueDate.BegOfPeriod));


			Assert.AreEqual(1848.4911476096459,Financial.PV(0.1/48,48,-100,3000,DueDate.EndOfPeriod));
			Assert.AreEqual(7278.0804243614521,Financial.PV(0.1/48,48,-100,-3000,DueDate.EndOfPeriod));

			Assert.AreEqual(1857.9979929971164,Financial.PV(0.1/48,48,-100,3000,DueDate.BegOfPeriod));
			Assert.AreEqual(7287.5872697489231,Financial.PV(0.1/48,48,-100,-3000,DueDate.BegOfPeriod));

			Assert.AreEqual(-5053.7378976476075,Financial.PV(-0.1/48,48,100,0,DueDate.EndOfPeriod));

			Assert.AreEqual(-5086.9414579281301,Financial.PV(-0.1/48,48.3,100,0,DueDate.EndOfPeriod));
		}

		#endregion
	}
}