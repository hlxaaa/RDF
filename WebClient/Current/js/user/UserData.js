//clear

$(document).ready(function () {

    init()
    $('.li-data').addClass('open')
    $('.a-userdata').addClass('active')

    $('.select-type').change(function () {
        changePage2()
    })

    $('#btn-refresh').click(function () {
        changePage2();
    })

    $('.select-user').change(function () {
        changePage2()
    })

    $('.Birthday').change(function () {
        changePage2()
    })


    //changePage();
    //var xs = ['00', '02', '04', '06', '08', '10', '12'];
    //xs.push('14')
    //var ys = [15, 16, 20, 25, 23, 25, 32];
    //ys.push(33.3)
    //changeData(xs, ys)
})

function init() {
    if (userId != 0) {
        $('.select-user').val(userId);
    }
    changePage2();
}


function changeData(xs, ys) {
    var BaseCompCharts = function () {
        var initChartsChartJS = function () {
            var $chartLinesCon = jQuery('.js-chartjs-lines')[0].getContext('2d');


            var $chartLines;
            var $chartLinesBarsRadarData;

            // Set global chart options
            var $globalOptions = {
                scaleFontFamily: "'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif",
                scaleFontColor: '#999',
                scaleFontStyle: '600',
                tooltipTitleFontFamily: "'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif",
                tooltipCornerRadius: 3,
                maintainAspectRatio: false,
                responsive: true
            };

            var $chartLinesBarsRadarData = {
                labels: xs,
                datasets: [
                    {
                        label: 'This Week',
                        fillColor: 'rgba(171, 227, 125, .3)',
                        strokeColor: 'rgba(171, 227, 125, 1)',
                        pointColor: 'rgba(171, 227, 125, 1)',
                        pointStrokeColor: '#fff',
                        pointHighlightFill: '#fff',
                        pointHighlightStroke: 'rgba(171, 227, 125, 1)',
                        data: ys
                    }
                ]
            };

            // Polar/Pie/Donut Data

            // Init Charts
            $chartLines = new Chart($chartLinesCon).Line($chartLinesBarsRadarData, $globalOptions);
            //$chartBars  = new Chart($chartBarsCon).Bar($chartLinesBarsRadarData, $globalOptions);
            //$chartRadar = new Chart($chartRadarCon).Radar($chartLinesBarsRadarData, $globalOptions);
            //$chartPolar = new Chart($chartPolarCon).PolarArea($chartPolarPieDonutData, $globalOptions);
            //$chartPie   = new Chart($chartPieCon).Pie($chartPolarPieDonutData, $globalOptions);
            //$chartDonut = new Chart($chartDonutCon).Doughnut($chartPolarPieDonutData, $globalOptions);
        };

        // jQuery Sparkline Charts, for more examples you can check out http://omnipotent.net/jquery.sparkline/#s-docs


        // Randomize Easy Pie Chart values
        var initRandomEasyPieChart = function () {
            jQuery('.js-pie-randomize').on('click', function () {
                jQuery(this)
                    .parents('.block')
                    .find('.pie-chart')
                    .each(function () {
                        var $random = Math.floor((Math.random() * 100) + 1);

                        jQuery(this)
                            .data('easyPieChart')
                            .update($random);
                    });
            });
        };

        // Flot charts, for more examples you can check out http://www.flotcharts.org/flot/examples/
        var initChartsFlot = function () {
            // Get the elements where we will attach the charts
            var $flotLines = jQuery('.js-flot-lines');

            // Init lines chart
            jQuery.plot($flotLines,
                [
                    {
                        label: 'Earnings',
                        data: $dataEarnings,
                        lines: {
                            show: true,
                            fill: true,
                            fillColor: {
                                colors: [{ opacity: .7 }, { opacity: .7 }]
                            }
                        },
                        points: {
                            show: true,
                            radius: 6
                        }
                    },
                    {
                        label: 'Sales',
                        data: $dataSales,
                        lines: {
                            show: true,
                            fill: true,
                            fillColor: {
                                colors: [{ opacity: .5 }, { opacity: .5 }]
                            }
                        },
                        points: {
                            show: true,
                            radius: 6
                        }
                    }
                ],
                {
                    colors: ['#abe37d', '#333333'],
                    legend: {
                        show: true,
                        position: 'nw',
                        backgroundOpacity: 0
                    },
                    grid: {
                        borderWidth: 0,
                        hoverable: true,
                        clickable: true
                    },
                    yaxis: {
                        tickColor: '#ffffff',
                        ticks: 3
                    },
                    xaxis: {
                        ticks: $dataMonths,
                        tickColor: '#f5f5f5'
                    }
                }
            );

            // Creating and attaching a tooltip to the classic chart
            var previousPoint = null, ttlabel = null;
            $flotLines.bind('plothover', function (event, pos, item) {
                if (item) {
                    if (previousPoint !== item.dataIndex) {
                        previousPoint = item.dataIndex;

                        jQuery('.js-flot-tooltip').remove();
                        var x = item.datapoint[0], y = item.datapoint[1];

                        if (item.seriesIndex === 0) {
                            ttlabel = '$ <strong>' + y + '</strong>';
                        } else if (item.seriesIndex === 1) {
                            ttlabel = '<strong>' + y + '</strong> sales';
                        } else {
                            ttlabel = '<strong>' + y + '</strong> tickets';
                        }

                        jQuery('<div class="js-flot-tooltip flot-tooltip">' + ttlabel + '</div>')
                            .css({ top: item.pageY - 45, left: item.pageX + 5 }).appendTo("body").show();
                    }
                }
                else {
                    jQuery('.js-flot-tooltip').remove();
                    previousPoint = null;
                }
            });

            // Stacked Chart
            jQuery.plot($flotStacked,
                [
                    {
                        label: 'Sales',
                        data: $dataSales
                    },
                    {
                        label: 'Earnings',
                        data: $dataEarnings
                    }
                ],
                {
                    colors: ['#faad7d', '#fadb7d'],
                    series: {
                        stack: true,
                        lines: {
                            show: true,
                            fill: true
                        }
                    },
                    lines: {
                        show: true,
                        lineWidth: 0,
                        fill: true,
                        fillColor: {
                            colors: [{ opacity: 1 }, { opacity: 1 }]
                        }
                    },
                    legend: {
                        show: true,
                        position: 'nw',
                        sorted: true,
                        backgroundOpacity: 0
                    },
                    grid: {
                        borderWidth: 0
                    },
                    yaxis: {
                        tickColor: '#ffffff',
                        ticks: 3
                    },
                    xaxis: {
                        ticks: $dataMonths,
                        tickColor: '#f5f5f5'
                    }
                }
            );

            // Live Chart
            var $dataLive = [];

            function getRandomData() { // Random data generator

                if ($dataLive.length > 0)
                    $dataLive = $dataLive.slice(1);

                while ($dataLive.length < 300) {
                    var prev = $dataLive.length > 0 ? $dataLive[$dataLive.length - 1] : 50;
                    var y = prev + Math.random() * 10 - 5;
                    if (y < 0)
                        y = 0;
                    if (y > 100)
                        y = 100;
                    $dataLive.push(y);
                }

                var res = [];
                for (var i = 0; i < $dataLive.length; ++i)
                    res.push([i, $dataLive[i]]);

                // Show live chart info
                jQuery('.js-flot-live-info').html(y.toFixed(0) + '%');

                return res;
            }

            function updateChartLive() { // Update live chart
                $chartLive.setData([getRandomData()]);
                $chartLive.draw();
                setTimeout(updateChartLive, 70);
            }

            var $chartLive = jQuery.plot($flotLive, // Init live chart
                [{ data: getRandomData() }],
                {
                    series: {
                        shadowSize: 0
                    },
                    lines: {
                        show: true,
                        lineWidth: 2,
                        fill: true,
                        fillColor: {
                            colors: [{ opacity: .2 }, { opacity: .2 }]
                        }
                    },
                    colors: ['#75b0eb'],
                    grid: {
                        borderWidth: 0,
                        color: '#aaaaaa'
                    },
                    yaxis: {
                        show: true,
                        min: 0,
                        max: 110
                    },
                    xaxis: {
                        show: false
                    }
                }
            );

            updateChartLive(); // Start getting new data

            // Bars Chart
            jQuery.plot($flotBars,
                [
                    {
                        label: 'Sales Before',
                        data: $dataSalesBefore,
                        bars: {
                            show: true,
                            lineWidth: 0,
                            fillColor: {
                                colors: [{ opacity: 1 }, { opacity: 1 }]
                            }
                        }
                    },
                    {
                        label: 'Sales After',
                        data: $dataSalesAfter,
                        bars: {
                            show: true,
                            lineWidth: 0,
                            fillColor: {
                                colors: [{ opacity: 1 }, { opacity: 1 }]
                            }
                        }
                    }
                ],
                {
                    colors: ['#faad7d', '#fadb7d'],
                    legend: {
                        show: true,
                        position: 'nw',
                        backgroundOpacity: 0
                    },
                    grid: {
                        borderWidth: 0
                    },
                    yaxis: {
                        ticks: 3,
                        tickColor: '#f5f5f5'
                    },
                    xaxis: {
                        ticks: $dataMonthsBars,
                        tickColor: '#f5f5f5'
                    }
                }
            );

            // Pie Chart
            jQuery.plot($flotPie,
                [
                    {
                        label: 'Sales',
                        data: 22
                    },
                    {
                        label: 'Tickets',
                        data: 22
                    },
                    {
                        label: 'Earnings',
                        data: 56
                    }
                ],
                {
                    colors: ['#fadb7d', '#75b0eb', '#abe37d'],
                    legend: { show: false },
                    series: {
                        pie: {
                            show: true,
                            radius: 1,
                            label: {
                                show: true,
                                radius: 2 / 3,
                                formatter: function (label, pieSeries) {
                                    return '<div class="flot-pie-label">' + label + '<br>' + Math.round(pieSeries.percent) + '%</div>';
                                },
                                background: {
                                    opacity: .75,
                                    color: '#000000'
                                }
                            }
                        }
                    }
                }
            );
        };

        return {
            init: function () {
                // Init all charts
                initChartsChartJS();
                //initChartsSparkline();
                //initChartsFlot();

                // Randomize Easy Pie values functionality
                initRandomEasyPieChart();
            }
        };
    }();

    jQuery(function () { BaseCompCharts.init(); });
}

function changePage() {
    jQuery.postNL('../userAjax/GetUserData', GetData(), function (data) {
        var xs = new Array();
        var ys = new Array();
        for (var i = 0; i < data.ListData.length; i++) {
            xs.push(data.ListData[i].x)
            ys.push(data.ListData[i].km)
        }
        console.log(xs)
        console.log(ys)
        changeData(xs, ys)
    })
}
function GetData() {
    var data = {
        userId: userId,
        date: "2018-02-26"
    }
    return data;
}


function changePage2() {
    jQuery.postNL('../userAjax/GetUserData', GetData2(), function (data) {
        var xs = new Array();
        var ys = new Array();
        switch ($('.select-type').val()) {
            case "km":
                for (var i = 0; i < data.ListData.length; i++) {
                    xs.push(data.ListData[i].x)
                    ys.push(data.ListData[i].km)
                }
                break;
            case "cal":
                for (var i = 0; i < data.ListData.length; i++) {
                    xs.push(data.ListData[i].x)
                    ys.push(data.ListData[i].cal)
                }
                break;
            case "tkm":
                for (var i = 0; i < data.ListData.length; i++) {
                    xs.push(data.ListData[i].x)
                    ys.push(data.ListData[i].tkm)
                }
                break;
        }

        console.log(xs)
        console.log(ys)
        changeData(xs, ys)
    })
}
function GetData2() {
    var data = {
        userId: $('.select-user').val(),
        date: $('.Birthday').val()
    }
    return data;
}
