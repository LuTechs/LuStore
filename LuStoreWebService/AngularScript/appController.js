(function() {
    angular
        .module('app', [])
        .controller('appController', appController);

    appController.$inject = ['$scope', '$location', '$http', '$httpParamSerializerJQLike'];

    function appController($scope, $location, $http, $httpParamSerializerJQLike) {
        var vm = this;
        vm.userName = 'admin';
        vm.password = '123';
        vm.description = '';
        vm.model = '';
        vm.brand = '';

        vm.getToken = function() {
            $http({
                url: $location.$$absUrl + 'token',
                method: 'POST',
                data: $httpParamSerializerJQLike({
                    username: vm.userName,
                    password: vm.password,
                    grant_type: 'password'
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            }).then(function(response) {
                if (response.status === 200) {
                    vm.token = 'bearer ' + response.data.access_token;
                }
            });
        }

        vm.getProducts = function() {
            $http({
                url: $location.$$absUrl + 'api/products',
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': vm.token
                }
            }).then(function(response) {
                    if (response.status === 200) {
                        vm.products = response.data;
                    }
                },
                function(err) {
                    vm.products = [];
                    vm.data = err.statusText;
                });
        }

        vm.getProductById = function() {
            $http({
                url: $location.$$absUrl + 'api/products/' + vm.productId,
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': vm.token
                }
            }).then(function(response) {
                    if (response.status === 200) {
                        vm.products = [];
                        vm.data = '';
                        vm.products.push(response.data);

                    }
                },
                function(err) {
                    vm.data = err.statusText;
                    vm.products = [];
                });
        }
        vm.getProductByFilter = function () {
            
            var queryString = 'description=' + vm.description+ '&model=' + vm.model+ '&brand=' + vm.brand;
            console.log(queryString);
            $http({
                url: $location.$$absUrl + 'api/products?'+queryString,
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': vm.token
                }
            }).then(function (response) {
                    if (response.status === 200) {
                        vm.products = response.data;
                        vm.data = '';
                    }
                },
                function (err) {
                    vm.data = err.statusText;
                    vm.products = [];
                });
        }
        vm.addNewProduct = function() {
            $http({
                url: $location.$$absUrl + 'api/products/',
                method: 'POST',
                data: vm.product,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': vm.token
                }
            }).then(function(response) {
                    if (response.status === 201) {
                        vm.product = {};
                        vm.getProducts();
                    }
                },
                function(err) {
                    vm.data = err.statusText;
                });
        }

        vm.editProduct=function(product) {
            vm.product = angular.copy(product);
        }
        vm.updateProduct=function() {
            $http({
                url: $location.$$absUrl + 'api/products/' + vm.product.id,
                method: 'PUT',
                data: vm.product,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': vm.token
                }
            }).then(function (response) {
                    if (response.status === 200) {
                        vm.product = {};
                        vm.getProducts();
                    }
                },
                function (err) {
                    vm.data = err.statusText;
                });
        }

        vm.deleteProduct = function(id) {
            $http({
                url: $location.$$absUrl + 'api/products/' + id,
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': vm.token
                }
            }).then(function(response) {
                    vm.getProducts();
                },
                function(err) {
                    vm.data = err.statusText;
                });
        }



    }
})();