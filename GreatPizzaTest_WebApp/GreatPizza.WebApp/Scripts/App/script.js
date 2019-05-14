var myApp = angular.module('myApp', []);

myApp.controller('mainController', function ($scope, $http) {

    $scope.Init = function () {
        $scope.LoadPizzas();
        $scope.LoadToppings();
    }

    $scope.Pizzas = [];
    $scope.Toppings = [];
    $scope.PizzaToppings = [];
    $scope.SelectedPizzaName = '';
    $scope.SelectedPizzaToppingName = '';
    $scope.SelectedPizzaNewToppingName = '';
    $scope.SelectedToppingName = '';
    $scope.NewToppingName = '';


    $scope.PizzaSelected = function () {
        var pizza = $scope.Pizzas.find(u => u.Name == $scope.SelectedPizzaName);
        $scope.PizzaToppings = pizza.Toppings;
    };

    $scope.AddTopping = function () {
        $http({
            method: 'POST',
            url: 'http://localhost:60070/api/Toppings/' + $scope.NewToppingName
        }).then(
        function (response) {
            $scope.NewToppingName = '';
            $scope.LoadToppings();
        },
        function (error) {
            alert(error.Message)
        });
    }

    $scope.AddToppingToPizza = function () {
        var pizza = $scope.Pizzas.find(u => u.Name == $scope.SelectedPizzaName);

        $http({
            method: 'POST',
            url: 'http://localhost:60070/api/Pizzas/' + pizza.Name + '/' + $scope.SelectedPizzaNewToppingName
        }).then(
        function (response) {
            $scope.LoadPizzas();
            $scope.LoadToppings();
        },
        function (error) {
            alert(error.Message)
        });
    }

    $scope.DeleteToppingFromPizza = function () {
        var pizza = $scope.Pizzas.find(u => u.Name == $scope.SelectedPizzaName);

        $http({
            method: 'DELETE',
            url: 'http://localhost:60070/api/Pizzas/' + pizza.Name + '/' + $scope.SelectedPizzaToppingName
        }).then(
        function (response) {
            if (response.data.Code == 200) {
                $scope.LoadPizzas();
                $scope.LoadToppings();
            }
        },
        function (error) {
            alert(error.Message)
        });
    }

    $scope.DeleteTopping = function () {
        $http({
            method: 'DELETE',
            url: 'http://localhost:60070/api/Toppings/' + $scope.SelectedToppingName
        }).then(
        function (response) {
            if (response.data.Code == 200) {
                $scope.LoadPizzas();
                $scope.LoadToppings();
            }
        },
        function (error) {
            alert(error.Message)
        });
    }


    $scope.LoadPizzas = function () {
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
    }
    
    $scope.LoadToppings = function () {
        $http({
            method: 'GET',
            url: 'http://localhost:60070/api/Toppings'
        }).then(
        function (response) {
            $scope.Toppings = response.data;
            $scope.SelectedToppingName = $scope.Toppings[0].Name;
        },
        function (error) {
            alert(error.Message)
        });
    } 

});