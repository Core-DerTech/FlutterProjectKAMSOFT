class Patient {
  final PatientName name;
  final int pessel;
  final DateTime dateOfBirth;
  final int diseaseDescription;

  Patient({
    required this.name,
    required this.pessel,
    required this.dateOfBirth,
    required this.diseaseDescription,
  });

  factory Patient.fromJson(Map<String, dynamic> json) {
    return Patient(
      name: PatientName.fromJson(json['name'] as Map<String, dynamic>),
      pessel: json['pessel'] as int,
      dateOfBirth: DateTime.parse(json['dateOfBirth'] as String),
      diseaseDescription: json['diseaseDescription'] as int,
    );
  }
}

class PatientName {
  final String firstName;
  final String lastName;

  PatientName({
    required this.firstName,
    required this.lastName,
  });

  factory PatientName.fromJson(Map<String, dynamic> json) {
    return PatientName(
      firstName: json['firstName'] as String,
      lastName: json['lastName'] as String,
    );
  }

  @override
  String toString() => '$firstName $lastName';
}