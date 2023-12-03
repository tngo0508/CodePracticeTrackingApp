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
                {
                    data: 'difficulty',
                    render: function (data) {
                        if (data.toLowerCase() === 'easy') {
                            return `<span class="badge bg-success">${data}</span>`
                        } else if (data.toLowerCase() === 'medium') {
                            return `<span class="badge bg-warning">${data}</span>`
                        } else {
                            return `<span class="badge bg-danger">${data}</span>`
                        }
                    }
                },
                {
                    data: 'frequency',
                    render: function (data, type, row, meta) {
                        //return `<progress class="progress-bar bg-info" role="progressbar" value="${data}" max="${maxFrequency}"></progress>`
                        return `<div class="progress">
                                  <div class="progress-bar bg-info" role="progressbar" style="width: ${parseFloat(data) / parseFloat(maxFrequency) * 100}%;" aria-valuenow="${data}" aria-valuemin="0" aria-valuemax="${maxFrequency}"></div>
                                </div>`
                    }
                },
                { data: 'lastUpdate' },
                { data: 'timing' },
                {
                    data: 'id',
                    render: function (data, type) {
                        return `<div class="w-75 btn-group" role="group">
                        <a href="/problem/upsert?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i>
                        </a>
                        <a href="/problem/delete/${data}" class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i>
                        </a>
                    </div>`
                    }
                }
            ],
            with: '100%'
        });
}

