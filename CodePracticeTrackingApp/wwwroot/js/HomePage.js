document.addEventListener('DOMContentLoaded', function () {
    //let typed = new Typed('#typed-output', {
    //    strings: ["<span>Welcome to CodeTrack</span>", "<span>Solve Problem</span>", "<span>Track Progress</span>", "<span>Check Statistic</span>", "<span>Ace Interview</span>"],
    //    typeSpeed: 100,
    //    backSpeed: 55,
    //    startDelay: 500,
    //    backDelay: 1000,
    //    loop: true,
    //    showCursor: false,
    //});
});
// demo chart
// Example data for charts
const barChartData = {
    labels: ['Easy', 'Medium', 'Hard'],
    datasets: [{
        label: 'Frequency',
        data: [30, 50, 20],
        backgroundColor: ['#4bc0c0', '#ffce56', '#ff6384'],
        borderWidth: 1
    }]
};

const radarChartData = {
    labels: ['Array', 'Linked List', 'Dynamic Programming', 'Greedy', 'Binary Search'],
    datasets: [{
        label: 'Strength',
        data: [80, 60, 70, 40, 90],
        backgroundColor: 'rgba(255, 99, 132, 0.2)',
        borderColor: 'rgba(255, 99, 132, 1)',
        borderWidth: 1
    }]
};

const pieChartData = {
    labels: ['Easy', 'Medium', 'Hard'],
    datasets: [{
        data: [30, 50, 20],
        backgroundColor: ['#36a2eb', '#ffce56', '#ff6384']
    }]
};

const bubbleChartData = {
    datasets: [{
        label: 'Bubble Chart',
        data: [{
            x: 10,
            y: 20,
            r: 15
        }, {
            x: 30,
            y: 40,
            r: 25
        }, {
            x: 50,
            y: 60,
            r: 20
        }],
        backgroundColor: 'rgba(75, 192, 192, 0.2)',
        borderColor: 'rgba(75, 192, 192, 1)',
        borderWidth: 1
    }]
};

//Create charts
const barChart = new Chart(document.getElementById('barChartDemo').getContext('2d'), {
    type: 'bar',
    data: barChartData,
    options: {
        //animation: {
        //    duration: 2000,
        //}
        responsive: true,
        maintainAspectRatio: false
    }
});

const radarChart = new Chart(document.getElementById('radarChartDemo').getContext('2d'), {
    type: 'radar',
    data: radarChartData,
    options: {
        //animation: {
        //    duration: 2000,
        //}
        responsive: true,
        maintainAspectRatio: false
    }
});

const pieChart = new Chart(document.getElementById('pieChartDemo').getContext('2d'), {
    type: 'pie',
    data: pieChartData,
    options: {
        //animation: {
        //    duration: 2000,
        //}
    }
});

const bubbleChart = new Chart(document.getElementById('bubbleChartDemo').getContext('2d'), {
    type: 'bubble',
    data: bubbleChartData,
    options: {
        //animation: {
        //    duration: 2000,
        //}
    }
});