
using JaratKezeloProject;

namespace TestJaratKezeloProject
{
    [TestFixture]
    public class JaratKezeloTests
    {
        private JaratKezelo _jaratKezelo;

        [SetUp]
        public void SetUp()
        {
            _jaratKezelo = new JaratKezelo();
        }

        [Test]
        public void UjJarat_HelyesAdatokkal_Sikeres()
        {
            _jaratKezelo.UjJarat("J123", "BUD", "LHR", new DateTime(2024, 6, 1, 12, 0, 0));
            Assert.DoesNotThrow(() => _jaratKezelo.MikorIndul("J123"));
        }

        [Test]
        public void UjJarat_DuplikaltJaratSzam_KivetelDob()
        {
            _jaratKezelo.UjJarat("J123", "BUD", "LHR", new DateTime(2024, 6, 1, 12, 0, 0));
            Assert.Throws<ArgumentException>(() => _jaratKezelo.UjJarat("J123", "BUD", "LHR", new DateTime(2024, 6, 1, 14, 0, 0)));
        }

        [Test]
        public void Keses_PozitivKeses_Sikeres()
        {
            _jaratKezelo.UjJarat("J123", "BUD", "LHR", new DateTime(2024, 6, 1, 12, 0, 0));
            _jaratKezelo.Keses("J123", 30);
            Assert.AreEqual(new DateTime(2024, 6, 1, 12, 30, 0), _jaratKezelo.MikorIndul("J123"));
        }

        [Test]
        public void Keses_NegativKeses_Sikeres()
        {
            _jaratKezelo.UjJarat("J123", "BUD", "LHR", new DateTime(2024, 6, 1, 12, 0, 0));
            _jaratKezelo.Keses("J123", 30);
            _jaratKezelo.Keses("J123", -15);
            Assert.AreEqual(new DateTime(2024, 6, 1, 12, 15, 0), _jaratKezelo.MikorIndul("J123"));
        }

        [Test]
        public void Keses_TulNagyNegativKeses_KivetelDob()
        {
            _jaratKezelo.UjJarat("J123", "BUD", "LHR", new DateTime(2024, 6, 1, 12, 0, 0));
            Assert.Throws<ArgumentException>(() => _jaratKezelo.Keses("J123", -30));
        }

        [Test]
        public void JaratokRepuloterrol_HelyesKimenet()
        {
            _jaratKezelo.UjJarat("J123", "BUD", "LHR", new DateTime(2024, 6, 1, 12, 0, 0));
            _jaratKezelo.UjJarat("J124", "BUD", "CDG", new DateTime(2024, 6, 1, 14, 0, 0));
            _jaratKezelo.UjJarat("J125", "LHR", "BUD", new DateTime(2024, 6, 1, 16, 0, 0));
            var jaratok = _jaratKezelo.JaratokRepuloterrol("BUD");
            CollectionAssert.Contains(jaratok, "J123");
            CollectionAssert.Contains(jaratok, "J124");
            CollectionAssert.DoesNotContain(jaratok, "J125");
        }
    }
}
