import 'package:flutter/material.dart';

import '../contratos/modelos.dart';
import '../temas/tema.dart';

/// Panel de resultados con las 8 métricas (rejilla de tarjetas).
class PanelResultados extends StatelessWidget {
  const PanelResultados({
    super.key,
    required this.resultado,
    required this.cargando,
    required this.error,
  });

  final ResultadoCombate? resultado;
  final bool cargando;
  final String? error;

  @override
  Widget build(BuildContext context) {
    final tema = TemaAlcance.de(context);
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text('RESULTADOS DE COMBATE', style: inter(tamano: 20, peso: 700, color: Colors.white, espaciado: 2)),
        const SizedBox(height: 18),
        if (cargando)
          Text('CALCULANDO...', style: inter(tamano: 13, color: textoSuave, espaciado: 1.2)),
        if (!cargando && error != null)
          Text(error!, style: inter(tamano: 14, color: tema.acentoFuerte)),
        if (!cargando && error == null && resultado != null) ...[
          _rejilla(tema, resultado!.metricas),
          const SizedBox(height: 18),
          Text(
            '${resultado!.iteracionesEjecutadas} iteraciones · ${resultado!.duracionMilisegundos} ms',
            style: inter(tamano: 12, color: textoTenue, espaciado: 1),
          ),
        ],
      ],
    );
  }

  Widget _rejilla(TemaMathHammer tema, Metricas metricas) {
    final tarjetas = <Widget>[
      _Tarjeta(titulo: 'Impactos esperados', valor: _decimal(metricas.impactosEsperados)),
      _Tarjeta(titulo: 'Heridas esperadas', valor: _decimal(metricas.heridasEsperadas)),
      _Tarjeta(titulo: 'Salvaciones del enemigo', valor: _decimal(metricas.salvacionesEnemigo)),
      _Tarjeta(titulo: 'Prob. de matar unidad', valor: '${(metricas.probabilidadMatarUnidad * 100).toStringAsFixed(1)}%'),
      _Tarjeta(titulo: 'Miniaturas eliminadas', valor: _decimal(metricas.miniaturasEliminadas)),
      _Tarjeta(titulo: 'Daño medio esperado', valor: _decimal(metricas.danioMedioEsperado)),
      _Tarjeta(titulo: 'P25 (Rango mínimo)', valor: _decimal(metricas.percentil25)),
      _Tarjeta(titulo: 'P75 (Rango máximo)', valor: _decimal(metricas.percentil75)),
    ];

    return LayoutBuilder(
      builder: (context, restricciones) {
        const separacion = 14.0;
        final ancho = (restricciones.maxWidth - separacion) / 2;
        return Wrap(
          spacing: separacion,
          runSpacing: separacion,
          children: [
            for (final tarjeta in tarjetas) SizedBox(width: ancho, child: tarjeta),
          ],
        );
      },
    );
  }

  String _decimal(double valor) => valor.toStringAsFixed(2);
}

class _Tarjeta extends StatelessWidget {
  const _Tarjeta({required this.titulo, required this.valor});

  final String titulo;
  final String valor;

  @override
  Widget build(BuildContext context) {
    final tema = TemaAlcance.de(context);
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 15),
      decoration: BoxDecoration(
        gradient: const LinearGradient(
          begin: Alignment.topLeft,
          end: Alignment.bottomRight,
          colors: [Color(0xF01E2123), Color(0xE60D0F11)],
        ),
        border: Border(
          top: BorderSide(color: tema.acento),
          bottom: BorderSide(color: tema.acento),
        ),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(titulo.toUpperCase(), style: inter(tamano: 11, color: textoClaro, espaciado: 0.8)),
          const SizedBox(height: 8),
          Text(valor, style: inter(tamano: 26, peso: 700, color: Colors.white)),
        ],
      ),
    );
  }
}