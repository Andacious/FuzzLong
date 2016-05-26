using NUnit.Framework;

namespace FuzzLong.UnitTests
{
    [TestFixture]
    public class FuzzLongAlgorithmTests
    {
        private FuzzLongAlgorithm _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new FuzzLongAlgorithm();
        }

        [Test]
        public void Compare_NullXString_NonNullYString_Zero()
        {
            Assert.That(_sut.Compare(null, "asdf"), Is.Zero);
        }

        [Test]
        public void Compare_NonNullXString_NullYString_Zero()
        {
            Assert.That(_sut.Compare("asdf", null), Is.Zero);
        }

        [Test]
        public void Compare_EqualStrings_One()
        {
            Assert.That(_sut.Compare("asdf", "asdf"), Is.EqualTo(1));
        }

        [TestCase("abcdefghij", "abcdefghij", ExpectedResult = 1)]
        [TestCase("abcdefghij", "abcdefghik", ExpectedResult = .9)]
        [TestCase("abcdefghij", "abcdefghlk", ExpectedResult = .8)]
        [TestCase("abcdefghij", "abcdefgmlk", ExpectedResult = .7)]
        [TestCase("abcdefghij", "abcdefnmlk", ExpectedResult = .6)]
        [TestCase("abcdefghij", "abcdeonmlk", ExpectedResult = .5)]
        [TestCase("abcdefghij", "abcdponmlk", ExpectedResult = .4)]
        [TestCase("abcdefghij", "abcqponmlk", ExpectedResult = .3)]
        [TestCase("abcdefghij", "abrqponmlk", ExpectedResult = .2)]
        [TestCase("abcdefghij", "asrqponmlk", ExpectedResult = .1)]
        public double Compare_SameCase_PercentEqual(string x, string y)
        {
            return _sut.Compare(x, y);
        }

        [TestCase("abcdefghij", "ABCDEFGHIJ", ExpectedResult = 1)]
        [TestCase("abcdefghij", "ABCDEFGHIK", ExpectedResult = .9)]
        [TestCase("abcdefghij", "ABCDEFGHLK", ExpectedResult = .8)]
        [TestCase("abcdefghij", "ABCDEFGMLK", ExpectedResult = .7)]
        [TestCase("abcdefghij", "ABCDEFNMLK", ExpectedResult = .6)]
        [TestCase("abcdefghij", "ABCDEONMLK", ExpectedResult = .5)]
        [TestCase("abcdefghij", "ABCDPONMLK", ExpectedResult = .4)]
        [TestCase("abcdefghij", "ABCQPONMLK", ExpectedResult = .3)]
        [TestCase("abcdefghij", "ABRQPONMLK", ExpectedResult = .2)]
        [TestCase("abcdefghij", "ASRQPONMLK", ExpectedResult = .1)]
        public double Compare_DifferentCase_PercentEqual(string x, string y)
        {
            return _sut.Compare(x, y);
        }
    }
}
