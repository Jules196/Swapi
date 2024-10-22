﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwapiBackend.Mapping;
using SwapiBackend.Models;

namespace SwapiBackend.Mapping.Tests;

[TestClass]
public class JsonMappingTests
{
    private const string _allPersonJson = "{\r\n    \"count\": 82, \r\n    \"next\": \"https://swapi.dev/api/people/?page=2\", \r\n    \"previous\": null, \r\n    \"results\": [\r\n        {\r\n            \"name\": \"Luke Skywalker\", \r\n            \"height\": \"172\", \r\n            \"mass\": \"77\", \r\n            \"hair_color\": \"blond\", \r\n            \"skin_color\": \"fair\", \r\n            \"eye_color\": \"blue\", \r\n            \"birth_year\": \"19BBY\", \r\n            \"gender\": \"male\", \r\n            \"homeworld\": \"https://swapi.dev/api/planets/1/\", \r\n            \"films\": [\r\n                \"https://swapi.dev/api/films/1/\", \r\n                \"https://swapi.dev/api/films/2/\", \r\n                \"https://swapi.dev/api/films/3/\", \r\n                \"https://swapi.dev/api/films/6/\"\r\n            ], \r\n            \"species\": [], \r\n            \"vehicles\": [\r\n                \"https://swapi.dev/api/vehicles/14/\", \r\n                \"https://swapi.dev/api/vehicles/30/\"\r\n            ], \r\n            \"starships\": [\r\n                \"https://swapi.dev/api/starships/12/\", \r\n                \"https://swapi.dev/api/starships/22/\"\r\n            ], \r\n            \"created\": \"2014-12-09T13:50:51.644000Z\", \r\n            \"edited\": \"2014-12-20T21:17:56.891000Z\", \r\n            \"url\": \"https://swapi.dev/api/people/1/\"\r\n        }, \r\n        {\r\n            \"name\": \"C-3PO\", \r\n            \"height\": \"167\", \r\n            \"mass\": \"75\", \r\n            \"hair_color\": \"n/a\", \r\n            \"skin_color\": \"gold\", \r\n            \"eye_color\": \"yellow\", \r\n            \"birth_year\": \"112BBY\", \r\n            \"gender\": \"n/a\", \r\n            \"homeworld\": \"https://swapi.dev/api/planets/1/\", \r\n            \"films\": [\r\n                \"https://swapi.dev/api/films/1/\", \r\n                \"https://swapi.dev/api/films/2/\", \r\n                \"https://swapi.dev/api/films/3/\", \r\n                \"https://swapi.dev/api/films/4/\", \r\n                \"https://swapi.dev/api/films/5/\", \r\n                \"https://swapi.dev/api/films/6/\"\r\n            ], \r\n            \"species\": [\r\n                \"https://swapi.dev/api/species/2/\"\r\n            ], \r\n            \"vehicles\": [], \r\n            \"starships\": [], \r\n            \"created\": \"2014-12-10T15:10:51.357000Z\", \r\n            \"edited\": \"2014-12-20T21:17:50.309000Z\", \r\n            \"url\": \"https://swapi.dev/api/people/2/\"\r\n        }, \r\n        {\r\n            \"name\": \"R2-D2\", \r\n            \"height\": \"96\", \r\n            \"mass\": \"32\", \r\n            \"hair_color\": \"n/a\", \r\n            \"skin_color\": \"white, blue\", \r\n            \"eye_color\": \"red\", \r\n            \"birth_year\": \"33BBY\", \r\n            \"gender\": \"n/a\", \r\n            \"homeworld\": \"https://swapi.dev/api/planets/8/\", \r\n            \"films\": [\r\n                \"https://swapi.dev/api/films/1/\", \r\n                \"https://swapi.dev/api/films/2/\", \r\n                \"https://swapi.dev/api/films/3/\", \r\n                \"https://swapi.dev/api/films/4/\", \r\n                \"https://swapi.dev/api/films/5/\", \r\n                \"https://swapi.dev/api/films/6/\"\r\n            ], \r\n            \"species\": [\r\n                \"https://swapi.dev/api/species/2/\"\r\n            ], \r\n            \"vehicles\": [], \r\n            \"starships\": [], \r\n            \"created\": \"2014-12-10T15:11:50.376000Z\", \r\n            \"edited\": \"2014-12-20T21:17:50.311000Z\", \r\n            \"url\": \"https://swapi.dev/api/people/3/\"\r\n        }, \r\n        {\r\n            \"name\": \"Darth Vader\", \r\n            \"height\": \"202\", \r\n            \"mass\": \"136\", \r\n            \"hair_color\": \"none\", \r\n            \"skin_color\": \"white\", \r\n            \"eye_color\": \"yellow\", \r\n            \"birth_year\": \"41.9BBY\", \r\n            \"gender\": \"male\", \r\n            \"homeworld\": \"https://swapi.dev/api/planets/1/\", \r\n            \"films\": [\r\n                \"https://swapi.dev/api/films/1/\", \r\n                \"https://swapi.dev/api/films/2/\", \r\n                \"https://swapi.dev/api/films/3/\", \r\n                \"https://swapi.dev/api/films/6/\"\r\n            ], \r\n            \"species\": [], \r\n            \"vehicles\": [], \r\n            \"starships\": [\r\n                \"https://swapi.dev/api/starships/13/\"\r\n            ], \r\n            \"created\": \"2014-12-10T15:18:20.704000Z\", \r\n            \"edited\": \"2014-12-20T21:17:50.313000Z\", \r\n            \"url\": \"https://swapi.dev/api/people/4/\"\r\n        }, \r\n        {\r\n            \"name\": \"Leia Organa\", \r\n            \"height\": \"150\", \r\n            \"mass\": \"49\", \r\n            \"hair_color\": \"brown\", \r\n            \"skin_color\": \"light\", \r\n            \"eye_color\": \"brown\", \r\n            \"birth_year\": \"19BBY\", \r\n            \"gender\": \"female\", \r\n            \"homeworld\": \"https://swapi.dev/api/planets/2/\", \r\n            \"films\": [\r\n                \"https://swapi.dev/api/films/1/\", \r\n                \"https://swapi.dev/api/films/2/\", \r\n                \"https://swapi.dev/api/films/3/\", \r\n                \"https://swapi.dev/api/films/6/\"\r\n            ], \r\n            \"species\": [], \r\n            \"vehicles\": [\r\n                \"https://swapi.dev/api/vehicles/30/\"\r\n            ], \r\n            \"starships\": [], \r\n            \"created\": \"2014-12-10T15:20:09.791000Z\", \r\n            \"edited\": \"2014-12-20T21:17:50.315000Z\", \r\n            \"url\": \"https://swapi.dev/api/people/5/\"\r\n        }, \r\n        {\r\n            \"name\": \"Owen Lars\", \r\n            \"height\": \"178\", \r\n            \"mass\": \"120\", \r\n            \"hair_color\": \"brown, grey\", \r\n            \"skin_color\": \"light\", \r\n            \"eye_color\": \"blue\", \r\n            \"birth_year\": \"52BBY\", \r\n            \"gender\": \"male\", \r\n            \"homeworld\": \"https://swapi.dev/api/planets/1/\", \r\n            \"films\": [\r\n                \"https://swapi.dev/api/films/1/\", \r\n                \"https://swapi.dev/api/films/5/\", \r\n                \"https://swapi.dev/api/films/6/\"\r\n            ], \r\n            \"species\": [], \r\n            \"vehicles\": [], \r\n            \"starships\": [], \r\n            \"created\": \"2014-12-10T15:52:14.024000Z\", \r\n            \"edited\": \"2014-12-20T21:17:50.317000Z\", \r\n            \"url\": \"https://swapi.dev/api/people/6/\"\r\n        }, \r\n        {\r\n            \"name\": \"Beru Whitesun lars\", \r\n            \"height\": \"165\", \r\n            \"mass\": \"75\", \r\n            \"hair_color\": \"brown\", \r\n            \"skin_color\": \"light\", \r\n            \"eye_color\": \"blue\", \r\n            \"birth_year\": \"47BBY\", \r\n            \"gender\": \"female\", \r\n            \"homeworld\": \"https://swapi.dev/api/planets/1/\", \r\n            \"films\": [\r\n                \"https://swapi.dev/api/films/1/\", \r\n                \"https://swapi.dev/api/films/5/\", \r\n                \"https://swapi.dev/api/films/6/\"\r\n            ], \r\n            \"species\": [], \r\n            \"vehicles\": [], \r\n            \"starships\": [], \r\n            \"created\": \"2014-12-10T15:53:41.121000Z\", \r\n            \"edited\": \"2014-12-20T21:17:50.319000Z\", \r\n            \"url\": \"https://swapi.dev/api/people/7/\"\r\n        }, \r\n        {\r\n            \"name\": \"R5-D4\", \r\n            \"height\": \"97\", \r\n            \"mass\": \"32\", \r\n            \"hair_color\": \"n/a\", \r\n            \"skin_color\": \"white, red\", \r\n            \"eye_color\": \"red\", \r\n            \"birth_year\": \"unknown\", \r\n            \"gender\": \"n/a\", \r\n            \"homeworld\": \"https://swapi.dev/api/planets/1/\", \r\n            \"films\": [\r\n                \"https://swapi.dev/api/films/1/\"\r\n            ], \r\n            \"species\": [\r\n                \"https://swapi.dev/api/species/2/\"\r\n            ], \r\n            \"vehicles\": [], \r\n            \"starships\": [], \r\n            \"created\": \"2014-12-10T15:57:50.959000Z\", \r\n            \"edited\": \"2014-12-20T21:17:50.321000Z\", \r\n            \"url\": \"https://swapi.dev/api/people/8/\"\r\n        }, \r\n        {\r\n            \"name\": \"Biggs Darklighter\", \r\n            \"height\": \"183\", \r\n            \"mass\": \"84\", \r\n            \"hair_color\": \"black\", \r\n            \"skin_color\": \"light\", \r\n            \"eye_color\": \"brown\", \r\n            \"birth_year\": \"24BBY\", \r\n            \"gender\": \"male\", \r\n            \"homeworld\": \"https://swapi.dev/api/planets/1/\", \r\n            \"films\": [\r\n                \"https://swapi.dev/api/films/1/\"\r\n            ], \r\n            \"species\": [], \r\n            \"vehicles\": [], \r\n            \"starships\": [\r\n                \"https://swapi.dev/api/starships/12/\"\r\n            ], \r\n            \"created\": \"2014-12-10T15:59:50.509000Z\", \r\n            \"edited\": \"2014-12-20T21:17:50.323000Z\", \r\n            \"url\": \"https://swapi.dev/api/people/9/\"\r\n        }, \r\n        {\r\n            \"name\": \"Obi-Wan Kenobi\", \r\n            \"height\": \"182\", \r\n            \"mass\": \"77\", \r\n            \"hair_color\": \"auburn, white\", \r\n            \"skin_color\": \"fair\", \r\n            \"eye_color\": \"blue-gray\", \r\n            \"birth_year\": \"57BBY\", \r\n            \"gender\": \"male\", \r\n            \"homeworld\": \"https://swapi.dev/api/planets/20/\", \r\n            \"films\": [\r\n                \"https://swapi.dev/api/films/1/\", \r\n                \"https://swapi.dev/api/films/2/\", \r\n                \"https://swapi.dev/api/films/3/\", \r\n                \"https://swapi.dev/api/films/4/\", \r\n                \"https://swapi.dev/api/films/5/\", \r\n                \"https://swapi.dev/api/films/6/\"\r\n            ], \r\n            \"species\": [], \r\n            \"vehicles\": [\r\n                \"https://swapi.dev/api/vehicles/38/\"\r\n            ], \r\n            \"starships\": [\r\n                \"https://swapi.dev/api/starships/48/\", \r\n                \"https://swapi.dev/api/starships/59/\", \r\n                \"https://swapi.dev/api/starships/64/\", \r\n                \"https://swapi.dev/api/starships/65/\", \r\n                \"https://swapi.dev/api/starships/74/\"\r\n            ], \r\n            \"created\": \"2014-12-10T16:16:29.192000Z\", \r\n            \"edited\": \"2014-12-20T21:17:50.325000Z\", \r\n            \"url\": \"https://swapi.dev/api/people/10/\"\r\n        }\r\n    ]\r\n}";
    private const string _singlePersonJson = "{\r\n    \"name\": \"Luke Skywalker\", \r\n    \"height\": \"172\", \r\n    \"mass\": \"77\", \r\n    \"hair_color\": \"blond\", \r\n    \"skin_color\": \"fair\", \r\n    \"eye_color\": \"blue\", \r\n    \"birth_year\": \"19BBY\", \r\n    \"gender\": \"male\", \r\n    \"homeworld\": \"https://swapi.dev/api/planets/1/\", \r\n    \"films\": [\r\n        \"https://swapi.dev/api/films/1/\", \r\n        \"https://swapi.dev/api/films/2/\", \r\n        \"https://swapi.dev/api/films/3/\", \r\n        \"https://swapi.dev/api/films/6/\"\r\n    ], \r\n    \"species\": [], \r\n    \"vehicles\": [\r\n        \"https://swapi.dev/api/vehicles/14/\", \r\n        \"https://swapi.dev/api/vehicles/30/\"\r\n    ], \r\n    \"starships\": [\r\n        \"https://swapi.dev/api/starships/12/\", \r\n        \"https://swapi.dev/api/starships/22/\"\r\n    ], \r\n    \"created\": \"2014-12-09T13:50:51.644000Z\", \r\n    \"edited\": \"2014-12-20T21:17:56.891000Z\", \r\n    \"url\": \"https://swapi.dev/api/people/1/\"\r\n}";

    [TestMethod]
    public void ToPersonModelsTest()
    {
        // Arrange
        string json = _allPersonJson;

        // Act
        AllPersonsModel? result = json.ToAllPersonModels();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(82, result.Count);
    }

    [TestMethod]
    public void ToPersonModelTest()
    {
        // Arrange
        string json = _singlePersonJson;

        // Act
        PersonModel? result = json.ToPersonModel();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Luke Skywalker", result.Name);
    }
}
