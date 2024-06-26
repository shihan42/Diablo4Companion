using D4Companion.Entities;
using D4Companion.Helpers;
using D4Companion.Interfaces;
using D4Companion.Services;
using FuzzierSharp.SimilarityRatio.Scorer.StrategySensitive;
using FuzzierSharp.SimilarityRatio;
using System.Text.Json;
using FuzzierSharp;
using FuzzierSharp.SimilarityRatio.Scorer.Composite;

namespace D4Companion.Tests
{
    public class FuzzierSharpTests
    {
        private List<AffixInfo> _affixes = new List<AffixInfo>();
        private List<SigilInfo> _sigils = new List<SigilInfo>();
        private List<string> _affixDescriptions = new List<string>();
        private List<string> _sigilNames = new List<string>();
        private Dictionary<string, string> _affixMapDescriptionToId = new Dictionary<string, string>();
        private Dictionary<string, string> _sigilMapNameToId = new Dictionary<string, string>();
        /// <summary>
        /// Input text, expected affix id
        /// </summary>
        private Dictionary<string, string> _affixTestMappings = new Dictionary<string, string>();
        private Dictionary<string, string> _sigilTestMappings = new Dictionary<string, string>();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Init data
            InitAffixData();
            InitSigilData();
            InitTestData();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DefaultRatioScorerTest()
        {
            Assert.Multiple(() =>
            {
                foreach (var affixTest in _affixTestMappings)
                {
                    var result = Process.ExtractOne(affixTest.Key, _affixDescriptions, scorer: ScorerCache.Get<DefaultRatioScorer>());
                    var affixId = _affixMapDescriptionToId[result.Value];

                    Assert.That(affixId, Is.EqualTo(affixTest.Value), $"Input: {affixTest.Key}");
                }

                foreach (var sigilTest in _sigilTestMappings)
                {
                    var result = Process.ExtractOne(sigilTest.Key, _sigilNames, scorer: ScorerCache.Get<DefaultRatioScorer>());
                    var sigilId = _sigilMapNameToId[result.Value];

                    Assert.That(sigilId, Is.EqualTo(sigilTest.Value), $"Input: {sigilTest.Key}");
                }
            });
        }

        [Test]
        public void PartialRatioScorerTest()
        {
            Assert.Multiple(() =>
            {
                foreach (var affixTest in _affixTestMappings)
                {
                    var result = Process.ExtractOne(affixTest.Key, _affixDescriptions, scorer: ScorerCache.Get<PartialRatioScorer>());
                    var affixId = _affixMapDescriptionToId[result.Value];

                    Assert.That(affixId, Is.EqualTo(affixTest.Value), $"Input: {affixTest.Key}");
                }

                foreach (var sigilTest in _sigilTestMappings)
                {
                    var result = Process.ExtractOne(sigilTest.Key, _sigilNames, scorer: ScorerCache.Get<PartialRatioScorer>());
                    var sigilId = _sigilMapNameToId[result.Value];

                    Assert.That(sigilId, Is.EqualTo(sigilTest.Value), $"Input: {sigilTest.Key}");
                }
            });
        }

        [Test]
        public void TokenSetScorerTest()
        {
            Assert.Multiple(() =>
            {
                foreach (var affixTest in _affixTestMappings)
                {
                    var result = Process.ExtractOne(affixTest.Key, _affixDescriptions, scorer: ScorerCache.Get<TokenSetScorer>());
                    var affixId = _affixMapDescriptionToId[result.Value];

                    Assert.That(affixId, Is.EqualTo(affixTest.Value));
                }

                foreach (var sigilTest in _sigilTestMappings)
                {
                    var result = Process.ExtractOne(sigilTest.Key, _sigilNames, scorer: ScorerCache.Get<TokenSetScorer>());
                    var sigilId = _sigilMapNameToId[result.Value];

                    Assert.That(sigilId, Is.EqualTo(sigilTest.Value), $"Input: {sigilTest.Key}");
                }
            });
        }

        [Test]
        public void PartialTokenSetScorerTest()
        {
            Assert.Multiple(() =>
            {
                foreach (var affixTest in _affixTestMappings)
                {
                    var result = Process.ExtractOne(affixTest.Key, _affixDescriptions, scorer: ScorerCache.Get<PartialTokenSetScorer>());
                    var affixId = _affixMapDescriptionToId[result.Value];

                    Assert.That(affixId, Is.EqualTo(affixTest.Value), $"Input: {affixTest.Key}");
                }

                foreach (var sigilTest in _sigilTestMappings)
                {
                    var result = Process.ExtractOne(sigilTest.Key, _sigilNames, scorer: ScorerCache.Get<PartialTokenSetScorer>());
                    var sigilId = _sigilMapNameToId[result.Value];

                    Assert.That(sigilId, Is.EqualTo(sigilTest.Value), $"Input: {sigilTest.Key}");
                }
            });
        }

        [Test]
        public void TokenSortScorerTest()
        {
            Assert.Multiple(() =>
            {
                foreach (var affixTest in _affixTestMappings)
                {
                    var result = Process.ExtractOne(affixTest.Key, _affixDescriptions, scorer: ScorerCache.Get<TokenSortScorer>());
                    var affixId = _affixMapDescriptionToId[result.Value];

                    Assert.That(affixId, Is.EqualTo(affixTest.Value), $"Input: {affixTest.Key}");
                }

                foreach (var sigilTest in _sigilTestMappings)
                {
                    var result = Process.ExtractOne(sigilTest.Key, _sigilNames, scorer: ScorerCache.Get<TokenSortScorer>());
                    var sigilId = _sigilMapNameToId[result.Value];

                    Assert.That(sigilId, Is.EqualTo(sigilTest.Value), $"Input: {sigilTest.Key}");
                }
            });
        }

        [Test]
        public void PartialTokenSortScorerTest()
        {
            Assert.Multiple(() =>
            {
                foreach (var affixTest in _affixTestMappings)
                {
                    var result = Process.ExtractOne(affixTest.Key, _affixDescriptions, scorer: ScorerCache.Get<PartialTokenSortScorer>());
                    var affixId = _affixMapDescriptionToId[result.Value];

                    Assert.That(affixId, Is.EqualTo(affixTest.Value), $"Input: {affixTest.Key}");
                }

                foreach (var sigilTest in _sigilTestMappings)
                {
                    var result = Process.ExtractOne(sigilTest.Key, _sigilNames, scorer: ScorerCache.Get<PartialTokenSortScorer>());
                    var sigilId = _sigilMapNameToId[result.Value];

                    Assert.That(sigilId, Is.EqualTo(sigilTest.Value), $"Input: {sigilTest.Key}");
                }
            });
        }

        [Test]
        public void TokenAbbreviationScorerTest()
        {
            Assert.Multiple(() =>
            {
                foreach (var affixTest in _affixTestMappings)
                {
                    var result = Process.ExtractOne(affixTest.Key, _affixDescriptions, scorer: ScorerCache.Get<TokenAbbreviationScorer>());
                    var affixId = _affixMapDescriptionToId[result.Value];

                    Assert.That(affixId, Is.EqualTo(affixTest.Value), $"Input: {affixTest.Key}");
                }

                foreach (var sigilTest in _sigilTestMappings)
                {
                    var result = Process.ExtractOne(sigilTest.Key, _sigilNames, scorer: ScorerCache.Get<TokenAbbreviationScorer>());
                    var sigilId = _sigilMapNameToId[result.Value];

                    Assert.That(sigilId, Is.EqualTo(sigilTest.Value), $"Input: {sigilTest.Key}");
                }
            });
        }

        [Test]
        public void PartialTokenAbbreviationScorerTest()
        {
            Assert.Multiple(() =>
            {
                foreach (var affixTest in _affixTestMappings)
                {
                    var result = Process.ExtractOne(affixTest.Key, _affixDescriptions, scorer: ScorerCache.Get<PartialTokenAbbreviationScorer>());
                    var affixId = _affixMapDescriptionToId[result.Value];

                    Assert.That(affixId, Is.EqualTo(affixTest.Value), $"Input: {affixTest.Key}");
                }

                foreach (var sigilTest in _sigilTestMappings)
                {
                    var result = Process.ExtractOne(sigilTest.Key, _sigilNames, scorer: ScorerCache.Get<PartialTokenAbbreviationScorer>());
                    var sigilId = _sigilMapNameToId[result.Value];

                    Assert.That(sigilId, Is.EqualTo(sigilTest.Value), $"Input: {sigilTest.Key}");
                }
            });
        }

        [Test]
        public void WeightedRatioScorerTest()
        {
            Assert.Multiple(() =>
            {
                foreach (var affixTest in _affixTestMappings)
                {
                    var result = Process.ExtractOne(affixTest.Key, _affixDescriptions, scorer: ScorerCache.Get<WeightedRatioScorer>());
                    var affixId = _affixMapDescriptionToId[result.Value];

                    Assert.That(affixId, Is.EqualTo(affixTest.Value), $"Input: {affixTest.Key}");
                }

                foreach (var sigilTest in _sigilTestMappings)
                {
                    var result = Process.ExtractOne(sigilTest.Key, _sigilNames, scorer: ScorerCache.Get<WeightedRatioScorer>());
                    var sigilId = _sigilMapNameToId[result.Value];

                    Assert.That(sigilId, Is.EqualTo(sigilTest.Value), $"Input: {sigilTest.Key}");
                }
            });
        }

        private void InitAffixData()
        {
            _affixes.Clear();
            string resourcePath = @$".\Data\Affixes.enUS.json";
            using (FileStream? stream = File.OpenRead(resourcePath))
            {
                if (stream != null)
                {
                    // create the options
                    var options = new JsonSerializerOptions()
                    {
                        WriteIndented = true
                    };
                    // register the converter
                    options.Converters.Add(new BoolConverter());
                    options.Converters.Add(new IntConverter());

                    _affixes = JsonSerializer.Deserialize<List<AffixInfo>>(stream, options) ?? new List<AffixInfo>();
                }
            }

            // Create affix description list for FuzzierSharp
            _affixDescriptions.Clear();
            _affixDescriptions = _affixes.Select(affix => affix.DescriptionClean).ToList();

            // Create dictionary to map affix description with affix id
            _affixMapDescriptionToId.Clear();
            _affixMapDescriptionToId = _affixes.ToDictionary(affix => affix.DescriptionClean, affix => affix.IdName);
        }

        private void InitSigilData()
        {
            _sigils.Clear();
            string resourcePath = @$".\Data\Sigils.enUS.json";
            using (FileStream? stream = File.OpenRead(resourcePath))
            {
                if (stream != null)
                {
                    // create the options
                    var options = new JsonSerializerOptions()
                    {
                        WriteIndented = true
                    };
                    // register the converter
                    options.Converters.Add(new BoolConverter());
                    options.Converters.Add(new IntConverter());

                    _sigils = JsonSerializer.Deserialize<List<SigilInfo>>(stream, options) ?? new List<SigilInfo>();
                }
            }

            // Create affix description list for FuzzierSharp
            _sigilNames.Clear();
            _sigilNames = _sigils.Select(affix => affix.Name).ToList();

            // Create dictionary to map affix description with affix id
            _sigilMapNameToId.Clear();
            _sigilMapNameToId = _sigils.ToDictionary(sigil => sigil.Name, sigil => sigil.IdName);
        }

        private void InitTestData()
        {
            // Key: Input text, Value: expected affix id
            _affixTestMappings = new Dictionary<string, string>
            {
                {"+715 Thorns","Thorns"},
                {"+715 Thoms","Thorns"},
                {"18 All Stats","CoreStats_All"},
                {"+10% Damage","Damage"},
                {"+19.5% Shadow Damage Over Time","Damage_Type_DoT_Bonus_Shadow"},
                {"+19.5% Fire Damage Over Time", "Damage_Type_DoT_Bonus_Burn"},
                {"4.5% Damage Reduction [3.1 - 7.6]%","DamageReduction"},
                {"16.7% Damage Reduction from Distant Enemies [10.3 - 17.4]%","DamageReductionDistant"}
            };
            _sigilTestMappings = new Dictionary<string, string>
            {
                {"monster cold resist","DungeonAffix_Minor_Monster_LessCold"}
            };
        }
    }
}