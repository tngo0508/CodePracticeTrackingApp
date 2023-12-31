﻿$(document).ready(() => {
    loadProblemTable();
});

let dataTable;

function createCharts(json) {
    createFrequencyChart(json);
    createDifficultyDistributionChart(json);
    createTimingChart(json);
    createBubbleChart(json);
    createRadarChart(json);
}

function loadProblemTable() {
    let maxFrequency =
    dataTable = $('#problem').DataTable({
            ajax: {
                'url': '/problem/ProblemList',
                'type': 'GET',
                'datatype': 'json',
                'dataSrc': function (json) {
                    maxFrequency = json.maxFrequency;
                    if (!_.isEmpty(json.data)) {
                        createCharts(json);
                    }

                    return json.data;
                }
            },
            "success": function (response) {
                // Handle success
                console.log("Data loaded successfully:", response);
            },
            "error": function (error) {
                // Handle error
                console.error("Error loading data:", error);
            },
            columns: [
                { data: 'title' },
                { data: 'tag' },
                {
                    data: 'difficulty',
                    render: function (data) {
                        if (data.toLowerCase() === 'easy') {
                            return `<span class="badge rounded-pill bg-success">${data}</span>`
                        } else if (data.toLowerCase() === 'medium') {
                            return `<span class="badge rounded-pill bg-warning">${data}</span>`
                        } else {
                            return `<span class="badge rounded-pill bg-danger">${data}</span>`
                        }
                    }
                },
                {
                    data: 'frequency',
                    render: function (data, type, row, meta) {
                        //return type === 'display'
                        //    ? '<progress value="' + data + '" max="${maxFrequency}"></progress>'
                        //    : data;
                        //return `<progress class="progress-bar" role="progressbar" value="${data}" max="${maxFrequency}"></progress>`
                        return type == 'display' ? `<div class="progress" title="${data}">
                                  <div class="progress-bar bg-info" role="progressbar" style="width: ${parseFloat(data) / parseFloat(maxFrequency) * 100}%;" aria-valuenow="${data}" aria-valuemin="0" aria-valuemax="${maxFrequency}"></div>
                                </div>`: data;
                    }
                },
                { data: 'lastUpdate' },
                {
                    data: 'timing',
                    render: function (data, type, row, meta) {
                        return type == 'display' ? data + ' mins': data;
                    }
                },
                {
                    data: 'id',
                    render: function (data, type) {
                        return `<div class="w-75 btn-group" role="group">
                        <a href="/problem/upsert?id=${data}" class="btn btn-primary mx-2" title="edit">
                            <i class="bi bi-pencil-square"></i>
                        </a>
                        
                        <a onClick=Delete("/problem/delete/${data}") class="btn btn-danger mx-2" title="delete">
                            <i class="bi bi-trash-fill"></i>
                        </a>
                    </div>`
                    }
                }
            ],
            with: '100%'
        });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete record!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}

