$(document).ready(() => {
    loadProblemTable();
});

function loadProblemTable() {
    let maxFrequency = 
    $('#problem').DataTable({
        ajax: {
            'url': '/problem/ProblemList',
            'type': 'GET',
            'datatype': 'json',
            'dataSrc': function (json) {
                maxFrequency = json.maxFrequency;
                return json.data
            }
        },
        columns: [
            { data: 'title' },
            { data: 'tag' },
            { data: 'difficulty' },
            {
                data: 'frequency',
                render: function (data, type, row, meta) {
                    console.log(data)
                    return `<progress value="${data}" max="${maxFrequency}"></progress>`
                }
            },
            { data: 'lastUpdate' },
            {
                data: 'id',
                render: function (data, type) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/problem/upsert?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a onClick=Delete("/problem/delete/${data}") class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>`
                }
            }
        ],
        with: '100%'
    });
}

