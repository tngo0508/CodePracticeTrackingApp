$(document).ready(() => {
    loadProblemTable();
});

function loadProblemTable() {
    $('#problem').DataTable({
        ajax: {
            'url': '/problem/ProblemList',
            'type': 'GET',
            'datatype': 'json'
        },
        columns: [
            { data: 'id' },
            { data: 'title' },
            { data: 'tag' },
            { data: 'difficulty' },
            { data: 'frequency' },
            { data: 'lastUpdate' },
        ],
        with: '100%'
    });
}

