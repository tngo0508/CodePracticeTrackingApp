let dataChart;
let data;
let currentChartType;
const ctx = document.getElementById('frequencyChart');

function createFrequencyChart(responseJson) {
    console.log(responseJson);
    data = responseJson;

    currentChartType = 'bar';

    let frequencies = responseJson.data.map(problem => problem.frequency);

    // create frequency chart
    dataChart = new Chart(ctx, {
        type: currentChartType,
        data: {
            labels: responseJson.data.map(problem => problem.title),
            datasets: [{
                label: 'Frequency',
                data: frequencies,
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    max: responseJson.maxFrequency
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: 'Frequency of Problems'
                },
                zoom: {
                    zoom: {
                        wheel: {
                            enabled: true,
                        },
                        pinch: {
                            enabled: true
                        },
                        mode: 'xy',
                        drag: {
                            enabled: true,
                            borderColor: 'rgba(225,225,225,0.3)',
                            borderWidth: 5,
                            backgroundColor: 'rgb(225,225,225)',
                            animationDuration: 1000,
                        },
                    }
                }
            }
        }
    });

}

function createDifficultyDistributionChart(responseJson) {
    // Extracting data for charts
    let titles = data.data.map(item => item.title);
    let frequencies = data.data.map(item => item.frequency);
    let difficulties = data.data.map(item => item.difficulty);

    // Group difficulties into three categories: Easy, Medium, Hard
    let groupedDifficulties = difficulties.map(difficulty => {
        if (difficulty === 'Easy') return 'Easy';
        else if (difficulty === 'Medium') return 'Medium';
        else return 'Hard';
    });

    // Pie chart for Difficulty Distribution
    let difficultyCtx = document.getElementById('difficultyChart').getContext('2d');
    let difficultyChart = new Chart(difficultyCtx, {
        type: 'pie',
        data: {
            labels: ['Easy', 'Medium', 'Hard'],
            datasets: [{
                data: [
                    groupedDifficulties.filter(difficulty => difficulty === 'Easy').length,
                    groupedDifficulties.filter(difficulty => difficulty === 'Medium').length,
                    groupedDifficulties.filter(difficulty => difficulty === 'Hard').length
                ],
                backgroundColor: [
                    'rgba(75, 191, 115, 0.5)',
                    'rgba(240, 173, 78, 0.5)',
                    'rgba(217, 83, 79, 0.5)'
                ],
                borderColor: [
                    'rgba(75, 191, 115,1)',
                    'rgba(240, 173, 78, 1)',
                    'rgba(217, 83, 79, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            plugins: {
                title: {
                    display: true,
                    text: 'Difficulty Distribution'
                }
            }
        }
    });
}

function createtimingChart(responseJson) {
    let titles = data.data.map(item => item.title);
    let timings = data.data.map(item => item.timing);

    let timingCtx = document.getElementById('timingChart').getContext('2d');
    let timingChart = new Chart(timingCtx, {
        type: 'line',
        data: {
            labels: titles,
            datasets: [{
                label: 'Timing',
                data: timings,
                fill: false,
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 2,
                pointBackgroundColor: 'rgba(75, 192, 192, 1)',
                pointRadius: 5,
                pointHoverRadius: 7
            }]
        },
        options: {
            scales: {
                y: {
                    title: {
                        display: true,
                        text: 'Timing (minutes)'
                    },
                    beginAtZero: true
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: 'Line Chart for Timing'
                },
                legend: {
                    display: true,
                    position: 'bottom'
                }
            }
        }
    });
}
function resetZoom() {
    dataChart.resetZoom();
}

function toggleChartType() {
    // Toggle between 'bar' and 'line' chart types
    currentChartType = currentChartType === 'bar' ? 'line' : 'bar';
    $('#toggleChart').text(currentChartType);

    // Destroy the existing chart
    dataChart.destroy();

    // Create a new chart with the updated chart type
    dataChart = new Chart(ctx, {
        type: currentChartType,
        data: {
            labels: data.data.map(problem => problem.title),
            datasets: [{
                label: 'Frequency',
                data: data.data.map(problem => problem.frequency),
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    max: data.maxFrequency
                }
            },
            plugins: {
                zoom: {
                    zoom: {
                        wheel: {
                            enabled: true,
                        },
                        pinch: {
                            enabled: true
                        },
                        mode: 'xy',
                        drag: {
                            enabled: true,
                            borderColor: 'rgba(225,225,225,0.3)',
                            borderWidth: 5,
                            backgroundColor: 'rgb(225,225,225)',
                            animationDuration: 1000,
                        },
                    }
                }
            }
        }
    });
}