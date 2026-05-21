class PatientResult {
  final int cipherType;
  final String patientName;
  final String formattedValue;
  final String statusColor;
  final bool isCritical;

  PatientResult({
    required this.cipherType,
    required this.patientName,
    required this.formattedValue,
    required this.statusColor,
    required this.isCritical,
  });

  factory PatientResult.fromJson(Map<String, dynamic> json) {
    return PatientResult(
      cipherType: json['cipherType'] as int? ?? 0,
      patientName: json['patientName']?.toString() ?? 'Unknown',
      formattedValue: json['formattedValue']?.toString() ?? 'N/A',
      statusColor: json['statusColor']?.toString() ?? 'Green',
      isCritical: json['isCritical'] as bool? ?? false,
    );
  }
}
