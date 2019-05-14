var myApp = angular.module('myApp', []);

myApp.controller('mainController', function ($scope, $http) {
    $scope.Pizzas = [];
    $scope.Toppings = [];
    $scope.PizzaToppings = [];
    $scope.SelectedPizzaName = '';
    $scope.SelectedToppingName = '';


    $scope.PizzaSelected = function () {
        var pizza = $scope.Pizzas.find(u => u.Name == $scope.SelectedPizzaName);
        $scope.PizzaToppings = pizza.Toppings;
    };

    $scope.DeleteToppingFromPizza = function () {
        var pizza = $scope.Pizzas.find(u => u.Name == $scope.SelectedPizzaName);

        $http({
            method: 'DELETE',
            url: 'http://localhost:60070/api/Pizzas/' + pizza.Name + '/' + $scope.SelectedToppingName
        }).then(
        function (response) {
            if (response.data.Code == 200) {
                var pizza = $scope.Pizzas.find(u => u.Name == $scope.SelectedPizzaName);
                pizza.Toppings = pizza.Toppings.filter(function (t) {
                    return t.Name !== $scope.SelectedToppingName;
                });
            }
        },
        function (error) {
            alert(error.Message)
        });
    }



    $http({
        method: 'GET',
        url: 'http://localhost:60070/api/Pizzas'
    }).then(
        function (response) {
            $scope.Pizzas = response.data;
            $scope.SelectedPizzaName = $scope.Pizzas[0].Name;
            $scope.PizzaSelected();
        },
        function (error) {
            alert(error.Message)
        });

    $http({
        method: 'GET',
        url: 'http://localhost:60070/api/Toppings'
    }).then(
        function (response) {
            $scope.Toppings = response.data;
        },
        function (error) {
            alert(error.Message)
        });



});