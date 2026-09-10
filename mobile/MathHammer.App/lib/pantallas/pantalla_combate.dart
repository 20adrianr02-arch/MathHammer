import 'package:flutter/material.dart';

import '../contratos/modelos.dart';
import '../servicios/cliente_api.dart';

/// Pantalla de combate: formulario de atacante y defensor, botón de cálculo
/// y panel de resultados con las 8 métricas.
class PantallaCombate extends StatefulWidget {
  const PantallaCombate({super.key, this.clienteApi});

  final ClienteApi? clienteApi;

  @override
  State<PantallaCombate> createState() => _PantallaCombateState();
}

class _PantallaCombateState extends State<PantallaCombate> {
  final _nombreAtacante = TextEditingController(text: 'Escuadra intercesora');
  final _cantidadAtaques = TextEditingController(text: '8');
  final _fuerza = TextEditingController(text: '5');
  final _penetracionArmadura = TextEditingController(text: '-2');
  final _danio = TextEditingController(text: '1');

  final _nombreDefensor = TextEditingController(text: 'Guerreros Necrones');
  final _resistencia = TextEditingController(text: '4');
  final _heridasPorMiniatura = TextEditingController(text: '2');
  final _cantidadMiniaturas = TextEditingController(text: '5');

  int _impactaA = 3;
  int _salvacion = 3;
  int? _salvacionInvulnerable;
  int? _sensacionDolor;

  bool _repiteParaImpactar = false;
  bool _twinLinked = false;
  bool _lance = false;
  bool _golpesLetales = false;
  bool _heridasDevastadoras = false;

  bool _reduccionDanio = false;
  bool _penalizacionImpactar = false;
  bool _penalizacionHerir = false;

  bool _cargando = false;
  String? _error;
  ResultadoCombate? _resultado;

  ClienteApi get _cliente => widget.clienteApi ?? ClienteApi();

  Future<void> _calcular() async {
    final peticion = _construirPeticion();
    if (peticion == null) {
      return;
    }

    setState(() {
      _cargando = true;
      _error = null;
      _resultado = null;
    });

    try {
      final resultado = await _cliente.simularCombate(peticion);
      if (!mounted) {
        return;
      }
      setState(() {
        _cargando = false;
        _resultado = resultado;
      });
    } on ApiException catch (excepcion) {
      if (!mounted) {
        return;
      }
      setState(() {
        _cargando = false;
        _error = excepcion.mensaje;
      });
    } catch (_) {
      if (!mounted) {
        return;
      }
      setState(() {
        _cargando = false;
        _error = 'No se pudo conectar con el servidor. Comprueba la API.';
      });
    }
  }

  PeticionCombate? _construirPeticion() {
    final cantidadAtaques = int.tryParse(_cantidadAtaques.text.trim());
    final fuerza = int.tryParse(_fuerza.text.trim());
    final penetracionArmadura = int.tryParse(_penetracionArmadura.text.trim());
    final danio = int.tryParse(_danio.text.trim());
    final resistencia = int.tryParse(_resistencia.text.trim());
    final heridasPorMiniatura = int.tryParse(_heridasPorMiniatura.text.trim());
    final cantidadMiniaturas = int.tryParse(_cantidadMiniaturas.text.trim());

    if (cantidadAtaques == null || cantidadAtaques < 1) {
      _mostrarError('La cantidad de ataques debe ser al menos 1.');
      return null;
    }
    if (fuerza == null || fuerza < 1) {
      _mostrarError('La fuerza debe ser al menos 1.');
      return null;
    }
    if (penetracionArmadura == null) {
      _mostrarError('La penetración de armadura no es válida.');
      return null;
    }
    if (danio == null || danio < 1) {
      _mostrarError('El daño debe ser al menos 1.');
      return null;
    }
    if (resistencia == null || resistencia < 1) {
      _mostrarError('La resistencia debe ser al menos 1.');
      return null;
    }
    if (heridasPorMiniatura == null || heridasPorMiniatura < 1) {
      _mostrarError('Las heridas por miniatura deben ser al menos 1.');
      return null;
    }
    if (cantidadMiniaturas == null || cantidadMiniaturas < 1) {
      _mostrarError('La cantidad de miniaturas debe ser al menos 1.');
      return null;
    }

    return PeticionCombate(
      atacante: Atacante(
        nombreUnidad: _nombreAtacante.text.trim().isEmpty ? 'Atacante' : _nombreAtacante.text.trim(),
        impactaA: _impactaA,
        repiteParaImpactar: _repiteParaImpactar,
      ),
      arma: Arma(
        cantidadAtaques: cantidadAtaques,
        fuerza: fuerza,
        penetracionArmadura: penetracionArmadura,
        danio: danio,
        repetirTiradaHerida: _twinLinked,
        lanza: _lance,
        golpesLetales: _golpesLetales,
        heridasDevastadoras: _heridasDevastadoras,
      ),
      defensor: Defensor(
        nombreUnidad: _nombreDefensor.text.trim().isEmpty ? 'Defensor' : _nombreDefensor.text.trim(),
        resistencia: resistencia,
        salvacion: _salvacion,
        salvacionInvulnerable: _salvacionInvulnerable,
        sensacionDolor: _sensacionDolor,
        heridasPorMiniatura: heridasPorMiniatura,
        cantidadMiniaturas: cantidadMiniaturas,
        reduccionDanio: _reduccionDanio,
        penalizacionImpactar: _penalizacionImpactar,
        penalizacionHerir: _penalizacionHerir,
      ),
      iteraciones: 10000,
    );
  }

  void _mostrarError(String mensaje) {
    ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(mensaje)));
  }

  @override
  void dispose() {
    _nombreAtacante.dispose();
    _cantidadAtaques.dispose();
    _fuerza.dispose();
    _penetracionArmadura.dispose();
    _danio.dispose();
    _nombreDefensor.dispose();
    _resistencia.dispose();
    _heridasPorMiniatura.dispose();
    _cantidadMiniaturas.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(16),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              _cabecera(),
              const SizedBox(height: 16),
              _panelAtacante(),
              const SizedBox(height: 16),
              _panelDefensor(),
              const SizedBox(height: 20),
              FilledButton(
                onPressed: _cargando ? null : _calcular,
                style: FilledButton.styleFrom(
                  backgroundColor: const Color(0xFFc3272b),
                  padding: const EdgeInsets.symmetric(vertical: 16),
                  textStyle: const TextStyle(fontWeight: FontWeight.bold, letterSpacing: 2),
                ),
                child: Text(_cargando ? 'CALCULANDO...' : 'CALCULAR COMBATE'),
              ),
              if (_error != null) ...[
                const SizedBox(height: 16),
                Text(_error!, textAlign: TextAlign.center, style: const TextStyle(color: Colors.redAccent)),
              ],
              if (_resultado != null) ...[
                const SizedBox(height: 20),
                _panelResultados(_resultado!),
              ],
            ],
          ),
        ),
      ),
    );
  }

  Widget _cabecera() {
    return Column(
      children: [
        Text(
          'MathHammer',
          textAlign: TextAlign.center,
          style: TextStyle(
            fontSize: 36,
            fontWeight: FontWeight.w800,
            letterSpacing: 2,
            color: Theme.of(context).colorScheme.primary,
          ),
        ),
        const SizedBox(height: 4),
        const Text(
          'SIMULADOR DE COMBATE 40K',
          textAlign: TextAlign.center,
          style: TextStyle(fontSize: 11, letterSpacing: 2, color: Colors.white70),
        ),
      ],
    );
  }

  Widget _panelAtacante() {
    return _panel(
      'ATACANTE',
      children: [
        _campoTexto(_nombreAtacante, 'Nombre de unidad'),
        _campoNumero(_cantidadAtaques, 'Cantidad de ataques'),
        _selector(_impactaA, 'Impacta a', 2, 6, (valor) => _impactaA = valor),
        _campoNumero(_fuerza, 'Fuerza'),
        _campoNumero(_penetracionArmadura, 'AP'),
        _campoNumero(_danio, 'Daño'),
        _habilidad('REPITE PARA IMPACTAR', _repiteParaImpactar, (v) => _repiteParaImpactar = v),
        _habilidad('TWIN-LINKED', _twinLinked, (v) => _twinLinked = v),
        _habilidad('LANCE (+1 AL HERIR)', _lance, (v) => _lance = v),
        _habilidad('LETHAL HITS', _golpesLetales, (v) => _golpesLetales = v),
        _habilidad('DEVASTATING WOUNDS', _heridasDevastadoras, (v) => _heridasDevastadoras = v),
      ],
    );
  }

  Widget _panelDefensor() {
    return _panel(
      'DEFENSOR',
      children: [
        _campoTexto(_nombreDefensor, 'Nombre de unidad'),
        _campoNumero(_resistencia, 'Resistencia'),
        _selector(_salvacion, 'Salvación', 2, 6, (valor) => _salvacion = valor),
        _selectorNullable(_salvacionInvulnerable, 'Salvación invulnerable', (valor) => _salvacionInvulnerable = valor),
        _selectorNullable(_sensacionDolor, 'Feel No Pain', (valor) => _sensacionDolor = valor, base: 3),
        _campoNumero(_heridasPorMiniatura, 'Heridas por miniatura'),
        _campoNumero(_cantidadMiniaturas, 'Cantidad de miniaturas'),
        _habilidad('-1 AL DAÑO', _reduccionDanio, (v) => _reduccionDanio = v),
        _habilidad('-1 AL IMPACTAR', _penalizacionImpactar, (v) => _penalizacionImpactar = v),
        _habilidad('-1 AL HERIR', _penalizacionHerir, (v) => _penalizacionHerir = v),
      ],
    );
  }

  Widget _panel(String titulo, {required List<Widget> children}) {
    return Container(
      padding: const EdgeInsets.all(14),
      decoration: BoxDecoration(
        color: const Color(0xFF111416),
        border: Border(top: BorderSide(color: Theme.of(context).colorScheme.primary, width: 1)),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          Text(titulo, style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold, letterSpacing: 2)),
          const SizedBox(height: 12),
          ...children,
        ],
      ),
    );
  }

  Widget _campoTexto(TextEditingController controlador, String etiqueta) {
    return TextField(
      controller: controlador,
      decoration: _decoracion(etiqueta),
    );
  }

  Widget _campoNumero(TextEditingController controlador, String etiqueta) {
    return TextField(
      controller: controlador,
      keyboardType: const TextInputType.numberWithOptions(signed: true),
      decoration: _decoracion(etiqueta),
    );
  }

  InputDecoration _decoracion(String etiqueta) {
    return InputDecoration(
      labelText: etiqueta,
      labelStyle: const TextStyle(fontSize: 12, letterSpacing: 1, color: Colors.white70),
      filled: true,
      fillColor: const Color(0xFF0c0e10),
      border: OutlineInputBorder(borderSide: const BorderSide(color: Color(0xFF484d4e))),
      enabledBorder: OutlineInputBorder(borderSide: const BorderSide(color: Color(0xFF484d4e))),
    );
  }

  Widget _selector(int valor, String etiqueta, int desde, int hasta, ValueChanged<int> alCambiar) {
    return DropdownButtonFormField<int>(
      initialValue: valor,
      decoration: _decoracion(etiqueta),
      items: [for (var opcion = desde; opcion <= hasta; opcion++) DropdownMenuItem(value: opcion, child: Text('$opcion+'))],
      onChanged: (nuevo) {
        if (nuevo != null) {
          setState(() => alCambiar(nuevo));
        }
      },
    );
  }

  Widget _selectorNullable(int? valor, String etiqueta, ValueChanged<int?> alCambiar, {int base = 2}) {
    return DropdownButtonFormField<int?>(
      initialValue: valor,
      decoration: _decoracion(etiqueta),
      items: [
        const DropdownMenuItem<int?>(value: null, child: Text('Ninguna')),
        for (var opcion = base; opcion <= 6; opcion++) DropdownMenuItem<int?>(value: opcion, child: Text('$opcion+')),
      ],
      onChanged: (nuevo) => setState(() => alCambiar(nuevo)),
    );
  }

  Widget _habilidad(String texto, bool activa, ValueChanged<bool> alCambiar) {
    return Material(
      color: Colors.transparent,
      child: CheckboxListTile(
        value: activa,
        onChanged: (nuevo) => setState(() => alCambiar(nuevo ?? false)),
        title: Text(texto, style: const TextStyle(fontSize: 13, letterSpacing: 1)),
        contentPadding: EdgeInsets.zero,
        dense: true,
        controlAffinity: ListTileControlAffinity.leading,
      ),
    );
  }

  Widget _panelResultados(ResultadoCombate resultado) {
    final metricas = resultado.metricas;
    return Container(
      padding: const EdgeInsets.all(14),
      decoration: BoxDecoration(
        color: const Color(0xFF111416),
        border: Border(top: BorderSide(color: Theme.of(context).colorScheme.primary, width: 1)),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const Text('RESULTADOS DE COMBATE', style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold, letterSpacing: 2)),
          const SizedBox(height: 12),
          _tarjeta('Impactos esperados', _decimal(metricas.impactosEsperados)),
          _tarjeta('Heridas esperadas', _decimal(metricas.heridasEsperadas)),
          _tarjeta('Salvaciones del enemigo', _decimal(metricas.salvacionesEnemigo)),
          _tarjeta('Prob. de matar unidad', '${(metricas.probabilidadMatarUnidad * 100).toStringAsFixed(1)}%'),
          _tarjeta('Miniaturas eliminadas', _decimal(metricas.miniaturasEliminadas)),
          _tarjeta('Daño medio esperado', _decimal(metricas.danioMedioEsperado)),
          _tarjeta('P25 (Rango mínimo)', _decimal(metricas.percentil25)),
          _tarjeta('P75 (Rango máximo)', _decimal(metricas.percentil75)),
          const SizedBox(height: 8),
          Text(
            '${resultado.iteracionesEjecutadas} iteraciones · ${resultado.duracionMilisegundos} ms',
            textAlign: TextAlign.center,
            style: const TextStyle(fontSize: 11, color: Colors.white70),
          ),
        ],
      ),
    );
  }

  Widget _tarjeta(String etiqueta, String valor) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 4),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(etiqueta, style: const TextStyle(fontSize: 13, color: Colors.white70)),
          Text(valor, style: const TextStyle(fontSize: 15, fontWeight: FontWeight.bold)),
        ],
      ),
    );
  }

  String _decimal(double valor) => valor.toStringAsFixed(2);
}