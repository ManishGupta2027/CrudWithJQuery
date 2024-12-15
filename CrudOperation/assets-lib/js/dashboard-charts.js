var ctx = document.getElementById('EarningChart');
var stackedLine = new Chart(ctx, {
    // The type of chart we want to create
    type: 'line',

    // The data for our dataset
    data: {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [{
            label: "Revenue",
            backgroundColor: 'rgb(255, 192, 192)',
            borderColor: 'rgb(210, 132, 132)',
            data: [0, 10, 5, 2, 20, 30, 45],
            pointStyle: 'rectRot',
            pointRadius: 5,
            borderWidth: 5,
            pointBorderColor: 'rgb(255, 255, 255)',
        }, {
            label: "Order",
            backgroundColor: 'rgb(216, 237, 211)',
            borderColor: 'rgb(157, 208, 145)',
            data: [10, 20, 25, 22, 30, 38, 65],
            pointStyle: 'rectRot',
            pointRadius: 5,
            borderWidth: 5,
            pointBorderColor: 'rgb(255, 255, 255)',
        }, {
            label: "Customers",
            backgroundColor: 'rgb(189, 206, 255)',
            borderColor: 'rgb(126, 155, 241)',
            data: [8, 50, 35, 20, 15, 88, 57],
            pointStyle: 'rectRot',
            pointRadius: 5,
            borderWidth: 5,
            pointBorderColor: 'rgb(255, 255, 255)',
        }, {
            label: "Sales",
            backgroundColor: 'rgb(250, 227, 176)',
            borderColor: 'rgb(216, 188, 128)',
            data: [18, 45, 65, 32, 28, 98, 63],
            pointStyle: 'rectRot',
            pointRadius: 5,
            borderWidth: 5,
            pointBorderColor: 'rgb(255, 255, 255)',
        }]
    },

    // Configuration options go here
    options: {
        responsive: true,
        legend: {
            fullWidth: true,
            labels: {
                boxWidth: 15,
            },
        },
        title: {
            display: true,
        },
        scales: {
            yAxes: [{
                stacked: false
            }]
        }
    }
});


// Site Visits Line Chart

var ctx = document.getElementById('siteVisitsChart');
var myLineChart = new Chart(ctx, {
    // The type of chart we want to create
    type: 'line',

    // The data for our dataset
    data: {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [{
            label: "New Visitor",
            data: [65, 59, 80, 81, 56, 55, 40],
            fill: false,
            borderColor: "rgb(75, 192, 192)",
            borderWidth: 5,
            pointStyle: 'rectRot',
            pointRadius: 5,
            pointBorderColor: 'rgb(0, 0, 0)',
            lineTension: 0
        }, {
            label: "Old Visitor",
            data: [45, 85, 55, 78, 89, 65, 20],
            fill: false,
            borderColor: "rgb(66, 197, 113)",
            borderWidth: 5,
            pointStyle: 'rectRot',
            pointRadius: 5,
            pointBorderColor: 'rgb(0, 0, 0)',
            lineTension: 0
        }]
    },
    options: {
        legend: {
            fullWidth: true,
            labels: {
                boxWidth: 15,
            },
        },
    }
});

// Marketing Campaign Line Chart

var ctx = document.getElementById('marketingCampaignChart');
var myBarChart = new Chart(ctx, {
    // The type of chart we want to create
    type: 'bar',

    // The data for our dataset
    data: {
        labels: ["August", "September", "October", "November"],
        datasets: [
            {
                data: [45, 85, 55, 78],
                backgroundColor: 'rgb(255, 192, 192)',
                borderColor: 'rgb(210, 132, 132)',
                borderWidth: 2,
                label: 'Searches'
            }, {
                data: [152, 25, 89, 85],
                backgroundColor: 'rgb(216, 237, 211)',
                borderColor: 'rgb(157, 208, 145)',
                borderWidth: 2,
                label: 'Products'
            }, {
                data: [55, 65, 25, 88],
                backgroundColor: 'rgb(189, 206, 255)',
                borderColor: 'rgb(126, 155, 241)',
                borderWidth: 2,
                label: 'Brands'
            }, {
                data: [150, 125, 89, 63],
                backgroundColor: 'rgb(250, 227, 176)',
                borderColor: 'rgb(216, 188, 128)',
                borderWidth: 2,
                label: 'Category'
            }]
    },
    options: {
        legend: {
            fullWidth: true,
            labels: {
                boxWidth: 15,
            },
        },
    }
});

var ctx = document.getElementById('topSearchChart');
var myLineChart = new Chart(ctx, {
    // The type of chart we want to create
    type: 'pie',

    // The data for our dataset
    data: {
        datasets: [{
            data: [45, 85, 55, 78],
            backgroundColor: ["rgb(255, 192, 192)", "rgb(192, 192, 192)", "rgb(255, 255, 192)", "rgb(255, 192, 255)"],
            label: 'Top Searches'
        }],
        labels: ["Searches", "Products", "Brands", "Category"]
    },
    options: {
        legend: {
            position: 'bottom',
            display: true,
            fullWidth: true,
            labels: {
                boxWidth: 15,
            },
        },
        title: {
            display: true,
        }
    }
});

var ctx = document.getElementById('deviceUsagesChart');
var myLineChart = new Chart(ctx, {
    // The type of chart we want to create
    type: 'doughnut',

    // The data for our dataset
    data: {
        datasets: [{
            data: [89, 72, 5, 2, 15],
            backgroundColor: ["rgb(255, 192, 192)", "rgb(216, 237, 211)", "rgb(255, 255, 192)", "rgb(255, 192, 255)", "rgba(255, 0, 0,0.5)"],
            label: 'Top Devices'
        }],
        labels: ["iOS", "Android", "Blackberry", "Symbian", "Others"]
    },
    options: {
        legend: {
            position: 'bottom',
            display: true,
            fullWidth: true,
            labels: {
                boxWidth: 15,
            },
        },
        title: {
            display: true,
        }
    }
});