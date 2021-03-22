import requests
import pytest

# Endereços nos quais eu subi os containers localmente.
uri_taxa_juros = 'http://localhost:5000/api/taxajuros'
uri_calculo_juros = 'http://localhost:8080/api/calculajuros'
uri_show_me_the_code = 'http://localhost:8080/api/showmethecode'

uri_git_repositorie = 'https://github.com/vitorhhelmbrecht/APICalculoJuros'
base_interest_rate = 0.01

wrong_parameters = [
    (100, 0),
    (0, 10),
    (0, 0),
]

calculos_juros_tests = [
    (100, 5, 105.1),
    (500, 10, 552.31),
    (250, 8, 270.71),
]


def converts_response_value_to_float(response):
    response_string = response.content.decode("utf-8")
    return float(response_string)


def test_get_taxa_juros_returns_ok():
    response = requests.get(uri_taxa_juros)
    assert response.ok


def test_get_taxa_juros_returns_the_interest_rate():
    response = requests.get(uri_taxa_juros)
    response_float = converts_response_value_to_float(response)
    assert response_float == base_interest_rate


@pytest.mark.parametrize("valor_inicial,meses", wrong_parameters)
def test_get_calcula_juros_return_bad_request(valor_inicial, meses):
    parameters = {"valorinicial": f"{valor_inicial}", "meses": f"{meses}"}
    response = requests.get(uri_calculo_juros, params=parameters)
    assert response.status_code == 400


def test_get_calcula_juros_return_ok():
    parameters = {"valorinicial": "100", "meses": "5"}
    response = requests.get(uri_calculo_juros, params=parameters)
    assert response.ok


@pytest.mark.parametrize("valor_inicial,meses,resultado_esperado", calculos_juros_tests)
def test_get_calcula_juros_return_right_value(valor_inicial, meses, resultado_esperado):
    parameters = {"valorinicial": f"{valor_inicial}", "meses": f"{meses}"}
    response = requests.get(uri_calculo_juros, params=parameters)
    response_float = converts_response_value_to_float(response)
    assert response_float == resultado_esperado


def test_get_showmethecode_link_is_right():
    response = requests.get(uri_show_me_the_code)
    response_string = response.content.decode("utf-8")
    assert response_string == uri_git_repositorie
