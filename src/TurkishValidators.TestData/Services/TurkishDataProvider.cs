using System;
using System.Collections.Generic;
using TurkishValidators.TestData.Generators;
using TurkishValidators.TestData.Models;

namespace TurkishValidators.TestData.Services
{
    /// <summary>
    /// Türk veri tipleri için test verisi sağlayıcısı.
    /// </summary>
    public class TurkishDataProvider
    {
        private readonly Random _random;
        private readonly TcKimlikNoGenerator _tcGenerator;
        private readonly VergiNoGenerator _vknGenerator;
        private readonly IbanGenerator _ibanGenerator;
        private readonly PlakaGenerator _plakaGenerator;
        private readonly TelefonNoGenerator _telefonGenerator;
        private readonly PostaKoduGenerator _postaKoduGenerator;

        public TurkishDataProvider()
        {
            _random = new Random();
            _tcGenerator = new TcKimlikNoGenerator(_random);
            _vknGenerator = new VergiNoGenerator(_random);
            _ibanGenerator = new IbanGenerator(_random);
            _plakaGenerator = new PlakaGenerator(_random);
            _telefonGenerator = new TelefonNoGenerator(_random);
            _postaKoduGenerator = new PostaKoduGenerator(_random);
        }

        public string GenerateTcKimlikNo() => _tcGenerator.Generate();
        public string GenerateVergiNo() => _vknGenerator.Generate();
        public string GenerateTurkishIban() => _ibanGenerator.Generate();
        public string GenerateVehiclePlate(string? cityName = null) => _plakaGenerator.Generate(cityName);
        public string GeneratePhoneGsm() => _telefonGenerator.GenerateGsm();
        public string GeneratePhoneLandline() => _telefonGenerator.GenerateLandline();
        public string GeneratePostalCode() => _postaKoduGenerator.Generate();

        /// <summary>
        /// Belirtilen sayıda toplu veri üretir.
        /// </summary>
        public List<GeneratedData> GenerateBulk(int count)
        {
            var list = new List<GeneratedData>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new GeneratedData
                {
                    TcKimlikNo = GenerateTcKimlikNo(),
                    VergiNo = GenerateVergiNo(),
                    Iban = GenerateTurkishIban(),
                    Plaka = GenerateVehiclePlate(),
                    Telefon = GeneratePhoneGsm(),
                    PostaKodu = GeneratePostalCode()
                });
            }
            return list;
        }
    }
}
