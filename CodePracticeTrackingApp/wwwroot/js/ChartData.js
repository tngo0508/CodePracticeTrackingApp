let frequencyChart;
let timingBarChart;
let timingChart;
let radarChart;
let difficultyChart;
let bubbleChart;
let data;
let currentChartType;
const freqquencyChartCtx = document.getElementById('frequencyChart');

function createFrequencyChart(responseJson) {
    if (frequencyChart) {
        frequencyChart.destroy();
    }
    console.log(responseJson);
    data = responseJson;

    currentChartType = 'bar';

    let frequencies = responseJson.data.map(problem => problem.frequency);

    // create frequency chart
    frequencyChart = new Chart(freqquencyChartCtx, {
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
                        //wheel: {
                        //    enabled: true,
                        //},
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
    if (difficultyChart) {
        difficultyChart.destroy();
    }
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
    difficultyChart = new Chart(difficultyCtx, {
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

function createTimingChart(responseJson) {
    if (timingChart) {
        timingChart.destroy();
    }
    let titles = responseJson.data.map(item => item.title);
    let timings = responseJson.data.map(item => item.timing);

    let timingCtx = document.getElementById('timingChart').getContext('2d');
    timingChart = new Chart(timingCtx, {
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
                    position: 'top'
                },
                zoom: {
                    pan: {
                        enabled: true,
                        mode: 'xy'
                    },
                    zoom: {
                        pinch: {
                            enabled: true
                        },
                        drag: {
                            enabled: true,
                            mode: 'xy'
                        }
                    }
                }
            }
        }
    });
}

function createBubbleChart(responsJson) {
    if (bubbleChart) {
        bubbleChart.destroy();
    }
    let titles = responsJson.data.map(item => item.title);
    let frequencies = responsJson.data.map(item => item.frequency);
    let timings = responsJson.data.map(item => item.timing);

    let bubbleCtx = document.getElementById('bubbleChart').getContext('2d');
    bubbleChart = new Chart(bubbleCtx, {
        type: 'bubble',
        data: {
            labels: titles,
            datasets: [{
                label: 'Frequency vs. Timing',
                data: data.data.map(item => ({
                    x: item.frequency,
                    y: item.timing,
                    r: 10 // Bubble radius
                })),
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 2
            }]
        },
        options: {
            scales: {
                x: {
                    title: {
                        display: true,
                        text: 'Frequency'
                    },
                    beginAtZero: true
                },
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
                    text: 'Bubble Chart for Frequency vs. Timing'
                },
                legend: {
                    display: true,
                    position: 'top'
                }
            }
        }
    });
}
// Function to generate random colors
function getRandomColor() {
    let letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}
function createTimingBarChart(responsJson) {
    if (timingBarChart) {
        timingBarChart.destroy();
    }
    // Extracting difficulty levels, frequencies, and timings
    let difficultyLevels = Array.from(new Set(responsJson.data.map(item => item.difficulty)));
    let frequencies = responsJson.data.map(item => item.frequency);
    let timings = responsJson.data.map(item => item.timing);

    // Creating a stacked bar chart
    let ctx = document.getElementById('timingBarChart').getContext('2d');
    timingBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: responsJson.data.map(item => item.title),
            datasets: [
            {
                label: 'Timing',
                data: timings,
                    borderWidth: 1,
                    borderColor: 'rgba(255, 99, 132, 0.7)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
            }]
        },
        options: {
            title: {
                display: true,
                fontSize: 16
            },
            legend: {
                display: true,
                position: 'bottom'
            }
        }
    });
}

function createRadarChart(responsJson) {
    if (radarChart) {
        radarChart.destroy();
    }
    // Grouping data by tags and summing up frequencies
    const groupedData = {};
    responsJson.data.forEach(item => {
        const tag = item.tag.toUpperCase();
        if (!groupedData[tag]) {
            groupedData[tag] = 0;
        }
        groupedData[tag] += item.frequency;
    });

    // Extracting tags and frequencies from grouped data
    const tags = Object.keys(groupedData);
    const frequencies = Object.values(groupedData);

    // Creating the radar chart
    const ctx = document.getElementById('radarChart').getContext('2d');
    radarChart = new Chart(ctx, {
        type: 'radar',
        data: {
            labels: tags,
            datasets: [{
                label: 'Frequency',
                data: frequencies,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 2,
            }]
        },
        options: {
            scale: {
                angleLines: {
                    display: false
                },
                ticks: {
                    suggestedMin: 0,
                    suggestedMax: responsJson.maxFrequency,
                    stepSize: 1
                }
            }
        }
    });
}

function resetZoom() {
    frequencyChart.resetZoom();
}

function resetZoomOnTimingChart() {
    timingChart.resetZoom();
}

function resetZoomDonut() {
    timeSeriesChart.resetZoom();
}

function toggleChartType() {
    // Toggle between 'bar' and 'line' chart types
    currentChartType = currentChartType === 'bar' ? 'line' : 'bar';
    $('#toggleChart').text(currentChartType);

    // Destroy the existing chart
    frequencyChart.destroy();

    // Create a new chart with the updated chart type
    frequencyChart = new Chart(freqquencyChartCtx, {
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