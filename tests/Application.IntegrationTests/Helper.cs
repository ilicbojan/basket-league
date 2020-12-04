using Application.Cities.Commands.CreateCity;
using Application.Countries.Commands.CreateCountry;
using Application.Field.Commands.CreateField;
using Application.Leagues.Commands.CreateLeague;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    using static Testing;

    public class Helper
    {
        public async static Task<int> CreateCountry()
        {
            var countryId = await SendAsync(new CreateCountryCommand
            {
                Name = "Srbija"
            });

            return countryId;
        }

        public async static Task<int> CreateCity()
        {
            var countryId = await CreateCountry();

            var cityId = await SendAsync(new CreateCityCommand
            {
                Name = "Beograd",
                CountryId = countryId
            });

            return cityId;
        }

        public async static Task<int> CreateField(int? cityId)
        {
            if (cityId == null)
            {
                cityId = await CreateCity();
            }

            var fieldId = await SendAsync(new CreateFieldCommand
            {
                Name = "Teren 1",
                Address = "Adresa terena",
                CityId = (int)cityId
            });

            return fieldId;
        }

        public async static Task<int> CreateLeague(int? cityId)
        {
            if (cityId == null)
            {
                cityId = await CreateCity();
            }

            var leagueId = await SendAsync(new CreateLeagueCommand
            {
                Name = "Prva liga",
                CityId = (int)cityId
            });

            return leagueId;
        }
    }
}
