EDITOR VERSION : 2022.3.7f1

# 사용법
Simulator~ 씬에 들어가서 Simulator라는 오브젝트의 Calculate Controller 스크립트 컴포넌트 변수를 조작하여 사용할 수 있습니다.
1. Calculate Time : 계산 시간으로, 실제 시간으로 몇초 시뮬레이션 계산을 할 건지 결정합니다.
2. Interval : 계산 간격으로 수학적 근사치를 구하기 위한 '매우 작은 값' 입니다. 0.01이하를 추천합니다.
3. Time Speed : 계산이 완료되고 결과를 확인할 때 배속을 설정할 수 있습니다.
4. is Calcualcate Complete : Do not touch
5. Progress Time : 시뮬레이션 상에서 계산이 끝나고 결과를 보여줄 때 얼마나 지났는지 보여줍니다. 입력하는 곳이 아닙니다.
6. Record Em : 물체의 역학적 에너지 데이터를 csv데이터로 출력할지 여부를 입력하는 곳입니다.
7. File Path : csv데이터 출력 위치를 입력하는 곳입니다.

굳이 미리 설정된 씬이 아니더라도 물체에 Attribute 스크립트를 컴포넌트로 넣고 값을 입력해주면 물체로 인식하고
Empty Object에 Calculate Controller 스크립트를 넣으면 시뮬레이터가 작동합니다.

# 그 외
- 아래 그래프는 전체 물체의 역학적 에너지의 합입니다. 이것으로 시뮬레이터의 오차를 확인할 수 있습니다.
