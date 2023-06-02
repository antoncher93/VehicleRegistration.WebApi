# Unit-тесты

## Зачем нужны

Unit-тесты, или модульные тесты, пишутся для проверки работы отдельных модулей и копонентов программного кода.
Модульный тест проверяет, что тестируемы модуль, выполняя определенное действие дает определенный результат.
Это позволяет вносить изменения в программный код не опасаясь того, что изменения повлияют на результат работы модуля.
Таким образом можно избежать программных ошибок при внесении изменений в код.

## Реализация 

### Тесты Api контроллеров

В нашем случае мы будем проверять работу контроллеров Web Api - `BrandControllerTests``. Прверяться будет то, 
что контроллеры при корректных запросах возвращают корректные данные, а при некорректных запросах возвращают результат
с кодом ошибки.

Пример теста:

```
    [Fact]
    public async Task GetMethodReturnsAllBrands()
    {
        var brands = new[]
        {
            new Brand() { Name = "Brand1" },
            new Brand() { Name = "Brand2" },
            new Brand() { Name = "Brand3" },
        };
        
        _brandRepository.SetupManyBrands(brands);

        var result = await _brandController.GetBrandsAsync();

        result
            .As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(brands);
    }
```
Данный тест проверят то, что GET метод контроллера BrandController возвращает `OK` (OkObjectResult) и все марки
автмобилей, которые есть в хранилище

### FakeRepository

Для создания объекта типа BrandController нужен объект, который реализует интерфейс хранилища `IBrandRepository`.
В тестах контроллеров нам не важно, какое хранилище использует контроллер, т.к. мы тестируем контроллер.
Обычно в таких случаях используют Mock-заглушки из библиотеки `Moq`, но их иногда сложно конфигурировать,
не всегда удобно читать и т.д. Поэтому мы будем использовать самописную фейковую реализацию хранилища `FakeBrandRepository`.

```
    public BrandControllerTests()
    {
        _brandRepository = new FakeBrandRepository();
        _brandController = new BrandController(_brandRepository);
    }
```

В такой реализации мы можем реализовать любое поведение хранилища и предоставить возможность его конфигурации, 
как например в `FakeBrandRepository` есть метод `SetupManyBrands`, который позволяет положить
в хранилище несколько марок автомобилей перед тем, как проверить, что контроллер `BrandController` благополучно их нам вернет.

## AAA и FluentAssertions

см. Интеграционыне тесты в документации VehicleRegistration.Desktop