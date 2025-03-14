
    document.addEventListener("DOMContentLoaded", function () {
        var fechas = window.chartData.fechas;
        var ingresos = window.chartData.ingresos;
        var gastos = window.chartData.gastos;
        var balance = window.chartData.balance;

                    // Gráfico de Ingresos y Gastos (Barras)
                    var ctx1 = document.getElementById("reporteGrafico").getContext("2d");
                    new Chart(ctx1, {
                        type: "bar",
                    data: {
                        labels: fechas,
                    datasets: [
                    {
                        label: "Ingresos",
                    data: ingresos,
                    backgroundColor: "rgba(0, 255, 0, 0.5)",
                    borderColor: "green",
                    borderWidth: 1
                    },
                    {
                        label: "Gastos",
                    data: gastos,
                    backgroundColor: "rgba(255, 0, 0, 0.5)",
                    borderColor: "red",
                    borderWidth: 1
                    }
                    ]
            },
                    options: {
                        responsive: true,
                    scales: {y: {beginAtZero: true } }
            }
        });

                    // Gráfico de Balance (Línea)
                    var ctx2 = document.getElementById("balanceGrafico").getContext("2d");
                    new Chart(ctx2, {
                        type: "line",
                    data: {
                        labels: fechas,
                    datasets: [
                    {
                        label: "Balance",
                    data: balance,
                    backgroundColor: "rgba(0, 0, 255, 0.3)",
                    borderColor: "blue",
                    borderWidth: 2,
                    fill: true
                    }
                    ]
            },
                    options: {
                        responsive: true,
                    scales: {y: {beginAtZero: false } }
            }
        });
    });
           