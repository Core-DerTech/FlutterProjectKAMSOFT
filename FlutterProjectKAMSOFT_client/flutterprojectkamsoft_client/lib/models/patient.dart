class Patient {
  final int cipherType;
  final PatientName name;
  final String pessel;
  final String dateOfBirth;
  final String diseaseDescription;
  final List<PatientAppointment> appointments;

  Patient({
    required this.cipherType,
    required this.name,
    required this.pessel,
    required this.dateOfBirth,
    required this.diseaseDescription,
    required this.appointments,
  });

  factory Patient.fromJson(Map<String, dynamic> json) {
    final appointmentsJson = json['appointments'] as List<dynamic>? ?? [];

    return Patient(
      cipherType: json['cipherType'] as int? ?? 0,
      name: PatientName.fromJson(json['name'] as Map<String, dynamic>),
      pessel: json['pessel']?.toString() ?? '',
      dateOfBirth: json['dateOfBirth']?.toString() ?? '',
      diseaseDescription: json['diseaseDescription']?.toString() ?? '',
      appointments: appointmentsJson
          .map((item) => PatientAppointment.fromJson(item as Map<String, dynamic>))
          .toList(),
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

class PatientAppointment {
  final String appointmentDate;
  final String description;
  final String title;
  final String type;

  PatientAppointment({
    required this.appointmentDate,
    required this.description,
    required this.title,
    required this.type,
  });

  factory PatientAppointment.fromJson(Map<String, dynamic> json) {
    return PatientAppointment(
      appointmentDate: json['appointmentDate']?.toString() ?? '',
      description: json['description']?.toString() ?? '',
      title: json['title']?.toString() ?? '',
      type: json['type']?.toString() ?? '',
    );
  }
}
