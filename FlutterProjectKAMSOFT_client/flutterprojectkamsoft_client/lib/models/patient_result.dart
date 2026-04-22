class PatientResult {
  final String patientName;
  final String formattedValue;
  final String statusColor;
  final bool isCritical;

  PatientResult({
    required this.patientName,
    required this.formattedValue,
    required this.statusColor,
    required this.isCritical,
  });

  factory PatientResult.fromJson(Map<String, dynamic> json) {
  return PatientResult(
    patientName: json['patientName'] ?? 'Unknown',
    formattedValue: json['formattedValue'] ?? 'N/A',
    statusColor: json['statusColor'] ?? 'Green',
    isCritical: json['isCritical'] ?? false,
  );
}
}