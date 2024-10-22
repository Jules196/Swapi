using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwapiBackend.DTOs;
using SwapiBackend.IO;
using SwapiBackend.Mapping;
using SwapiBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SwapiBackend.Tests;

[TestClass]
public class StarWarsWebDataTests
{
    private CancellationTokenSource _cts = new(20_000);
    private StarWarsData? starWarsData;

    [TestInitialize]
    public void Initialize()
    {
        starWarsData = new StarWarsData();
    }

    [TestMethod]
    public async Task GetAllPersonsAsyncTest()
    {
        _cts?.Cancel(true);
        _cts = new(15_000);

        Assert.IsNotNull(starWarsData);

        // Round 1 without caching
        List<PersonDTO>? result = await starWarsData.GetAllPersonNamesAsync(_cts.Token);
        Assert.IsNotNull(result);
        int resultCount = result.Count;
        Assert.IsTrue(resultCount > 0);

        // Round 2 with caching
        result = await starWarsData.GetAllPersonNamesAsync(_cts.Token);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
        Assert.AreEqual(resultCount, result.Count);

        // Round 3 with invalid data
        starWarsData.InvalidData = true;
        result = await starWarsData.GetAllPersonNamesAsync(_cts.Token);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
        Assert.AreEqual(resultCount, result.Count);
    }

    [TestMethod]
    public async Task GetAllPersonDetailsAsyncTest()
    {
        _cts?.Cancel(true);
        _cts = new(15_000);

        Assert.IsNotNull(starWarsData);

        // Round 1 without caching
        List<PersonDetailDTO>? result = await starWarsData.GetAllPersonDetailsAsync(_cts.Token);
        Assert.IsNotNull(result);
        int resultCount = result.Count;
        Assert.IsTrue(resultCount > 0);

        // Round 2 with caching
        result = await starWarsData.GetAllPersonDetailsAsync(_cts.Token);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
        Assert.AreEqual(resultCount, result.Count);

        // Round 3 with invalid data
        starWarsData.InvalidData = true;
        result = await starWarsData.GetAllPersonDetailsAsync(_cts.Token);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
        Assert.AreEqual(resultCount, result.Count);
    }
}