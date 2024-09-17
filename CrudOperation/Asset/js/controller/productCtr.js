myApp.controller('ProductController', ['$scope', function ($scope) {
    $scope.product = {};

    $scope.submitForm = function () {
        if ($scope.productForm.$valid) {
            var data = {
                title: $scope.product.title,
                stockCode: $scope.product.stockCode,
                price: $scope.product.price,
                category: $scope.product.category,
                gender: $scope.product.gender,
                isActive: $scope.product.isActive
            };

            // Make POST request using Axios
            axios.post('/Product/NewProduct', data, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(function (response) {
                    // Handle success response
                    if (response.data.isValid) {
                        alert(response.data.Message);
                        window.location.href = '/Product/GetProductList';
                    } else {
                        alert(response.data.Message);
                    }
                })
                .catch(function (error) {
                    // Handle error
                    console.error(error);
                });
        } else {
            // Form is invalid, display error messages
            $scope.productForm.$submitted = true;
        }
    };

}]);